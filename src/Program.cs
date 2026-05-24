// Program.cs — RealKIE-bench CLI entry.
using Microsoft.Extensions.Logging;
using RealKIEBench;

bool smoke = args.Contains("--smoke");
int trainN = smoke ? 10 : 75;
int testN = smoke ? 5 : 50;

var apiKey = EnvLoader.Get("ANTHROPIC_API_KEY");
if (string.IsNullOrWhiteSpace(apiKey))
{
    Console.Error.WriteLine("Set ANTHROPIC_API_KEY in .env or environment before running.");
    return 1;
}

using var loggerFactory = LoggerFactory.Create(b => b.AddConsole().SetMinimumLevel(LogLevel.Information));
var logger = loggerFactory.CreateLogger("RealKIE");

var dataDir = Path.Combine(AppContext.BaseDirectory, "data");
var trainRows = DataLoader.LoadCsv(Path.Combine(dataDir, "s1_train.csv"), trainN);
var testRows = DataLoader.LoadCsv(Path.Combine(dataDir, "s1_test.csv"), testN);
logger.LogInformation("Loaded {Train} train rows, {Test} test rows", trainRows.Count, testRows.Count);

var entityTypes = DataLoader.DiscoverEntityTypes(trainRows.Concat(testRows));
var entityTypesCsv = string.Join(", ", entityTypes);
logger.LogInformation("Entity types ({N}): {Types}", entityTypes.Count, entityTypesCsv);

var trainset = DataLoader.ToExamples(trainRows, entityTypesCsv);
var testset = DataLoader.ToExamples(testRows, entityTypesCsv);

// Skip task-stronger-than-reflect: no plausible upside from a weaker reflection model.
var combos = smoke
    ? new[] { (ModelChoice.Haiku, ModelChoice.Opus) }
    : new[]
    {
        (ModelChoice.Haiku,  ModelChoice.Haiku),
        (ModelChoice.Haiku,  ModelChoice.Sonnet),
        (ModelChoice.Haiku,  ModelChoice.Opus),
        (ModelChoice.Sonnet, ModelChoice.Sonnet),
        (ModelChoice.Sonnet, ModelChoice.Opus),
        (ModelChoice.Opus,   ModelChoice.Opus),
    };

var runner = new ComboRunner(apiKey, entityTypes, trainset, testset, logger);
var matrix = new MatrixRunner(runner, logger);
var results = await matrix.RunAsync(combos);

var resultsDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "results");
Directory.CreateDirectory(resultsDir);
var outPath = Path.Combine(resultsDir, smoke ? "smoke.md" : "benchmark.md");
var md = ResultsWriter.Render(results, entityTypes, trainset.Count, testset.Count, DateTime.UtcNow);
await File.WriteAllTextAsync(outPath, md);

logger.LogInformation("Wrote {Path} ({Bytes} bytes)", outPath, md.Length);

Console.WriteLine();
Console.WriteLine("================ MACRO F1 SUMMARY ================");
Console.WriteLine($"{"Task",-12} {"Reflect",-12} {"Baseline",10} {"GEPA",10} {"Δ (pp)",10}");
foreach (var r in results)
{
    double bl = r.Baseline.MacroF1();
    double opt = r.Optimized.MacroF1();
    Console.WriteLine($"{r.TaskLM,-12} {r.ReflectLM,-12} {bl,10:F3} {opt,10:F3} {(opt - bl) * 100,10:+0.0;-0.0;0.0}");
}

return 0;