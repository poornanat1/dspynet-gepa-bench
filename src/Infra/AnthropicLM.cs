// AnthropicLM.cs
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DSpyNet.DSPy.Core;

namespace RealKIEBench;

/// <summary>Minimal Anthropic Messages API client implementing ILM. Cribbed from DSpyNet integration tests.</summary>
public class AnthropicLM : ILM
{
    private readonly HttpClient _http;
    private readonly string _model;
    private readonly int _maxTokens;

    public AnthropicLM(string apiKey, string model = "claude-haiku-4-5", int maxTokens = 2048)
    {
        if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException("API key is required", nameof(apiKey));
        _model = model;
        _maxTokens = maxTokens;
        _http = new HttpClient { BaseAddress = new Uri("https://api.anthropic.com/"), Timeout = TimeSpan.FromMinutes(5) };
        _http.DefaultRequestHeaders.Add("x-api-key", apiKey);
        _http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
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
            using var req = new HttpRequestMessage(HttpMethod.Post, "v1/messages")
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
                throw new HttpRequestException($"Anthropic API {(int)resp.StatusCode}: {raw}");

            using var doc = JsonDocument.Parse(raw);
            var sb = new StringBuilder();
            foreach (var block in doc.RootElement.GetProperty("content").EnumerateArray())
            {
                if (block.TryGetProperty("type", out var t) && t.GetString() == "text" &&
                    block.TryGetProperty("text", out var text))
                {
                    sb.Append(text.GetString());
                }
            }
            return sb.ToString();
        }
        throw new HttpRequestException("Anthropic API: exhausted retries on 429/5xx.");
    }
}