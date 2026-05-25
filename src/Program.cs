// Program.cs — RealKIE-bench CLI entry.
using Microsoft.Extensions.Logging;
using RealKIEBench;

bool smoke = args.Contains("--smoke");
int trainN = smoke ? 10 : 75;
int testN = smoke ? 5 : 50;

// Provider: --provider=<name>, else $BENCH_PROVIDER, else auto-pick by which env key is set.
string? requested = args.FirstOrDefault(a => a.StartsWith("--provider="))?.Substring("--provider=".Length)
                   ?? EnvLoader.Get("BENCH_PROVIDER");
Provider? provider = requested != null
    ? Provider.FromName(requested)
    : (EnvLoader.Get("MISTRAL_API_KEY") is { Length: > 0 } ? Provider.Mistral
       : EnvLoader.Get("ANTHROPIC_API_KEY") is { Length: > 0 } ? Provider.Anthropic
       : null);

if (provider == null)
{
    Console.Error.WriteLine("No provider selected. Pass --provider=mistral|anthropic or set MISTRAL_API_KEY / ANTHROPIC_API_KEY in .env.");
    return 1;
}

var apiKey = EnvLoader.Get(provider.EnvKey);
if (string.IsNullOrWhiteSpace(apiKey))
{
    Console.Error.WriteLine($"Provider '{provider.Name}' selected but {provider.EnvKey} is unset.");
    return 1;
}

using var loggerFactory = LoggerFactory.Create(b => b.AddConsole().SetMinimumLevel(LogLevel.Information));
var logger = loggerFactory.CreateLogger("RealKIE");
logger.LogInformation("Provider = {Name}", provider.Name);

var dataDir = Path.Combine(AppContext.BaseDirectory, "data");
var trainRows = DataLoader.LoadCsv(Path.Combine(dataDir, "s1_train.csv"), trainN);
var testRows = DataLoader.LoadCsv(Path.Combine(dataDir, "s1_test.csv"), testN);
logger.LogInformation("Loaded {Train} train rows, {Test} test rows", trainRows.Count, testRows.Count);

var entityTypes = DataLoader.DiscoverEntityTypes(trainRows.Concat(testRows));
var entityTypesCsv = string.Join(", ", entityTypes);
logger.LogInformation("Entity types ({N}): {Types}", entityTypes.Count, entityTypesCsv);

var trainset = DataLoader.ToExamples(trainRows, entityTypesCsv);
var testset = DataLoader.ToExamples(testRows, entityTypesCsv);

// Tiers are weak → mid → strong; skip task-stronger-than-reflect (no plausible upside).
var weak   = provider.Tiers[0];
var mid    = provider.Tiers[1];
var strong = provider.Tiers[2];
var combos = smoke
    ? new[] { (weak, strong) }
    : new[]
    {
        (weak,   weak),
        (weak,   mid),
        (weak,   strong),
        (mid,    mid),
        (mid,    strong),
        (strong, strong),
    };

var runner = new ComboRunner(provider, apiKey, entityTypes, trainset, testset, logger);
var matrix = new MatrixRunner(runner, logger);
var results = await matrix.RunAsync(combos);

var resultsDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "results");
Directory.CreateDirectory(resultsDir);
var outPath = Path.Combine(resultsDir, smoke ? $"smoke-{provider.Name}.md" : $"benchmark-{provider.Name}.md");
var md = ResultsWriter.Render(results, entityTypes, trainset.Count, testset.Count, DateTime.UtcNow);
await File.WriteAllTextAsync(outPath, md);

logger.LogInformation("Wrote {Path} ({Bytes} bytes)", outPath, md.Length);

Console.WriteLine();
Console.WriteLine($"================ MACRO F1 SUMMARY ({provider.Name}) ================");
Console.WriteLine($"{"Task",-16} {"Reflect",-16} {"Baseline",10} {"GEPA",10} {"Δ (pp)",10}");
foreach (var r in results)
{
    double bl = r.Baseline.MacroF1();
    double opt = r.Optimized.MacroF1();
    Console.WriteLine($"{r.TaskLM,-16} {r.ReflectLM,-16} {bl,10:F3} {opt,10:F3} {(opt - bl) * 100,10:+0.0;-0.0;0.0}");
}

return 0;