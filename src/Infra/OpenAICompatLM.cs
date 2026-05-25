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

    public OpenAICompatLM(string baseUrl, string apiKey, string model, int maxTokens = 2048)
    {
        if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException("API key is required", nameof(apiKey));
        if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("Base URL is required", nameof(baseUrl));
        _model = model;
        _maxTokens = maxTokens;
        _http = new HttpClient { BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/"), Timeout = TimeSpan.FromMinutes(5) };
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

        for (int attempt = 0; attempt < 5; attempt++)
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
                await Task.Delay(TimeSpan.FromSeconds(Math.Min(waitSec, 30)));
                continue;
            }
            if (!resp.IsSuccessStatusCode)
                throw new HttpRequestException($"LM API {(int)resp.StatusCode}: {raw}");

            using var doc = JsonDocument.Parse(raw);
            var choices = doc.RootElement.GetProperty("choices");
            if (choices.GetArrayLength() == 0) return string.Empty;
            return choices[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
        }
        throw new HttpRequestException("LM API: exhausted retries on 429/5xx.");
    }
}