// Provider.cs — bundles an API endpoint, its tier list, and a factory.
using DSpyNet.DSPy.Core;

namespace RealKIEBench;

public record Provider(
    string Name,
    string EnvKey,
    Func<string, ModelChoice, ILM> Build,
    ModelChoice[] Tiers)
{
    public static readonly Provider Anthropic = new(
        Name: "anthropic",
        EnvKey: "ANTHROPIC_API_KEY",
        Build: (apiKey, mc) => new AnthropicLM(apiKey, mc.Slug, maxTokens: 4096),
        Tiers: new[]
        {
            new ModelChoice("claude-haiku-4-5",  "Haiku 4.5"),
            new ModelChoice("claude-sonnet-4-6", "Sonnet 4.6"),
            new ModelChoice("claude-opus-4-7",   "Opus 4.7"),
        });

    public static readonly Provider Mistral = new(
        Name: "mistral",
        EnvKey: "MISTRAL_API_KEY",
        // Per-tier minimum gap between calls — Mistral's RPS limits drop sharply at the upper tiers.
        Build: (apiKey, mc) =>
        {
            int minGapMs = mc.Slug switch
            {
                var s when s.StartsWith("mistral-large")  => 1500,
                var s when s.StartsWith("mistral-medium") => 1000,
                _                                         => 600,
            };
            return new OpenAICompatLM("https://api.mistral.ai/v1", apiKey, mc.Slug, maxTokens: 2048, minGapMs: minGapMs);
        },
        // Pinned to dated slugs: avoids -latest alias 503s, makes results reproducible.
        Tiers: new[]
        {
            new ModelChoice("mistral-small-2603",    "Mistral Small 4"),
            new ModelChoice("mistral-medium-3-5",    "Mistral Medium 3.5"),
            new ModelChoice("mistral-large-2512",    "Mistral Large (2512)"),
        });

    // Add more providers (Moonshot Kimi K2, OpenRouter, Qwen) by following the Mistral pattern with their /v1 base URL.

    public static Provider? FromName(string name) => name?.ToLowerInvariant() switch
    {
        "anthropic" => Anthropic,
        "mistral"   => Mistral,
        _ => null
    };
}