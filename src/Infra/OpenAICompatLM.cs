// OpenAICompatLM.cs — works for any OpenAI-compatible /v1/chat/completions endpoint (Mistral, Moonshot, OpenRouter, etc.).
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DSpyNet.DSPy.Core;

namespace RealKIEBench;

public class OpenAICompatLM : ILM
{
    private readonly HttpClient _http;
    private readonly string _model;
    private readonly int _maxTokens;
    private readonly TimeSpan _minGap;
    // Serializes concurrent callers (GEPA's parallel evaluator) and enforces a minimum gap between calls.
    private readonly SemaphoreSlim _gate = new(1, 1);
    private DateTime _lastCallUtc = DateTime.MinValue;

    public OpenAICompatLM(string baseUrl, string apiKey, string model, int maxTokens = 2048, int minGapMs = 0)
    {
        if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException("API key is required", nameof(apiKey));
        if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("Base URL is required", nameof(baseUrl));
        _model = model;
        _maxTokens = maxTokens;
        _minGap = TimeSpan.FromMilliseconds(Math.Max(0, minGapMs));
        // 3-min per-request timeout — long enough for Mistral's slow tail on large passages, short enough that retries fire in reasonable time.
        _http = new HttpClient { BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/"), Timeout = TimeSpan.FromSeconds(180) };
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    public async Task<string> GenerateAsync(string prompt, Dictionary<string, object>? kwargs = null)
    {
        var body = JsonSerializer.Serialize(new
        {
            model = _model,
            max_tokens = _maxTokens,
            messages = new[] { new { role = "user", content = prompt } }
        });

        await _gate.WaitAsync();
        try
        {
            if (_minGap > TimeSpan.Zero)
            {
                var since = DateTime.UtcNow - _lastCallUtc;
                if (since < _minGap) await Task.Delay(_minGap - since);
            }
            return await SendWithRetries(body);
        }
        finally
        {
            _lastCallUtc = DateTime.UtcNow;
            _gate.Release();
        }
    }

    private async Task<string> SendWithRetries(string body)
    {
        Exception? lastFailure = null;
        for (int attempt = 0; attempt < 8; attempt++)
        {
            try
            {
                using var req = new HttpRequestMessage(HttpMethod.Post, "chat/completions")
                {
                    Content = new StringContent(body, Encoding.UTF8, "application/json")
                };
                using var resp = await _http.SendAsync(req);
                var raw = await resp.Content.ReadAsStringAsync();

                if (resp.StatusCode == System.Net.HttpStatusCode.TooManyRequests || (int)resp.StatusCode >= 500)
                {
                    int waitSec = resp.Headers.TryGetValues("retry-after", out var ra) &&
                                  int.TryParse(ra.First(), out var s) ? s : (1 << attempt);
                    await Task.Delay(TimeSpan.FromSeconds(Math.Min(waitSec, 60)));
                    lastFailure = new HttpRequestException($"LM API {(int)resp.StatusCode}: {raw}");
                    continue;
                }
                if (!resp.IsSuccessStatusCode)
                    throw new HttpRequestException($"LM API {(int)resp.StatusCode}: {raw}");

                using var doc = JsonDocument.Parse(raw);
                var choices = doc.RootElement.GetProperty("choices");
                if (choices.GetArrayLength() == 0) return string.Empty;
                return choices[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
            }
            // Network-level failures (timeout, connection reset, DNS hiccup) — retriable like a 5xx.
            catch (OperationCanceledException oce)
            {
                lastFailure = oce;
                await Task.Delay(TimeSpan.FromSeconds(Math.Min(1 << attempt, 60)));
            }
            catch (HttpRequestException hre) when (hre.InnerException is System.Net.Sockets.SocketException or IOException)
            {
                lastFailure = hre;
                await Task.Delay(TimeSpan.FromSeconds(Math.Min(1 << attempt, 60)));
            }
        }
        throw new HttpRequestException($"LM API: exhausted retries. Last: {lastFailure?.Message}", lastFailure);
    }
}