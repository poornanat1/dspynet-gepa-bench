// ComboRunner.cs
using DSpyNet.DSPy.Core;
using DSpyNet.DSPy.Evaluation;
using DSpyNet.DSPy.Modules;
using DSpyNet.DSPy.Teleprompters;
using Microsoft.Extensions.Logging;

namespace RealKIEBench;

/// <summary>One (task LM, reflection LM) combo: GEPA compile + held-out test eval.</summary>
public class ComboRunner
{
    private readonly Provider _provider;
    private readonly string _apiKey;
    private readonly List<string> _entityTypes;
    private readonly List<Example> _trainset;
    private readonly List<Example> _testset;
    private readonly ILogger _logger;

    public ComboRunner(Provider provider, string apiKey, List<string> entityTypes,
        List<Example> trainset, List<Example> testset, ILogger logger)
    {
        _provider = provider;
        _apiKey = apiKey;
        _entityTypes = entityTypes;
        _trainset = trainset;
        _testset = testset;
        _logger = logger;
    }

    /// <summary>Default-instruction baseline for one task LM.</summary>
    public async Task<PerTypeSpanCorpus> ComputeBaselineAsync(ModelChoice task)
    {
        var lm = _provider.Build(_apiKey, task);
        var student = new Predict<S1ExtractionSig>(lm, _logger);
        return await EvaluateCorpusAsync(student, $"baseline[{task.DisplayName}]");
    }

    /// <summary>GEPA compile + test eval. Returns the test corpus and the final instruction.</summary>
    public async Task<(PerTypeSpanCorpus Optimized, string Instruction)> CompileAndEvaluateAsync(
        ModelChoice task, ModelChoice reflect)
    {
        var taskLM = _provider.Build(_apiKey, task);
        var reflectionLM = _provider.Build(_apiKey, reflect);

        var student = new Predict<S1ExtractionSig>(taskLM, _logger);
        var metric = F1Metric.Build(_entityTypes);

        var gepa = new GEPA<Predict<S1ExtractionSig>>(
            reflectionLM: reflectionLM,
            metric: metric,
            options: new GEPAOptions
            {
                Auto = GEPAAutoBudget.Light,
                ReflectionMinibatchSize = 3,
                ValidationSplit = 0.3,
                Seed = 42,
                UseMerge = true,
                MergeEvery = 5
            },
            logger: _logger);

        _logger.LogInformation("[combo task={Task} reflect={Reflect}] starting GEPA", task.DisplayName, reflect.DisplayName);
        var optimized = await gepa.CompileAsync(student, _trainset);
        var corpus = await EvaluateCorpusAsync(optimized, $"optimized[{task.DisplayName}/{reflect.DisplayName}]");
        return (corpus, optimized.State.Instruction);
    }

    // Sequential to preserve API rate-limit headroom across concurrent combos.
    private async Task<PerTypeSpanCorpus> EvaluateCorpusAsync(Module program, string tag)
    {
        var corpus = new PerTypeSpanCorpus();
        for (int i = 0; i < _testset.Count; i++)
        {
            try
            {
                var pred = (Prediction)await program.InvokeAsync(_testset[i]);
                var spans = F1Metric.ExtractPerExampleSpans(_testset[i], pred, _entityTypes);
                foreach (var t in _entityTypes) corpus.Add(t, spans[t].Gold, spans[t].Pred);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("[{Tag}] doc {I} failed: {Msg}", tag, i, ex.Message);
                // Real gold + empty pred so a failure scores as full miss, not a free pass.
                var emptyPred = new Prediction(new Dictionary<string, object> { ["Extractions"] = "{}" });
                var spans = F1Metric.ExtractPerExampleSpans(_testset[i], emptyPred, _entityTypes);
                foreach (var t in _entityTypes) corpus.Add(t, spans[t].Gold, spans[t].Pred);
            }
        }
        _logger.LogInformation("[{Tag}] macro F1 = {F1:F3}", tag, corpus.MacroF1());
        return corpus;
    }
}