// MatrixRunner.cs
using Microsoft.Extensions.Logging;

namespace RealKIEBench;

/// <summary>Runs every combo; shares one baseline per task LM to halve baseline cost.</summary>
public class MatrixRunner
{
    private readonly ComboRunner _runner;
    private readonly ILogger _logger;

    public MatrixRunner(ComboRunner runner, ILogger logger)
    {
        _runner = runner;
        _logger = logger;
    }

    public async Task<List<ComboResult>> RunAsync(IEnumerable<(ModelChoice Task, ModelChoice Reflect)> combos)
    {
        var combosList = combos.ToList();
        var baselines = new Dictionary<string, PerTypeSpanCorpus>();
        var results = new List<ComboResult>();

        foreach (var task in combosList.Select(c => c.Task).DistinctBy(t => t.Slug))
        {
            _logger.LogInformation("=== Baseline: task LM = {Name} ===", task.DisplayName);
            try
            {
                baselines[task.Slug] = await _runner.ComputeBaselineAsync(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Baseline failed for {Name}", task.DisplayName);
                baselines[task.Slug] = new PerTypeSpanCorpus();
            }
        }

        foreach (var (task, reflect) in combosList)
        {
            _logger.LogInformation("=== Combo: task={Task} reflect={Reflect} ===", task.DisplayName, reflect.DisplayName);
            try
            {
                var (optimized, instruction) = await _runner.CompileAndEvaluateAsync(task, reflect);
                results.Add(new ComboResult(task.DisplayName, reflect.DisplayName,
                    baselines[task.Slug], optimized, instruction, null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Combo failed");
                results.Add(new ComboResult(task.DisplayName, reflect.DisplayName,
                    baselines[task.Slug], new PerTypeSpanCorpus(), "", ex.Message));
            }
        }

        return results;
    }
}