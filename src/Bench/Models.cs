// Models.cs — shared result/config types.
namespace RealKIEBench;

/// <summary>Accumulated TP/FP/FN per entity type across a test corpus; yields micro/macro F1.</summary>
public class PerTypeSpanCorpus
{
    private readonly Dictionary<string, (int Tp, int Fp, int Fn)> _counts = new();

    public void Add(string entityType, HashSet<string> gold, HashSet<string> pred)
    {
        int tp = gold.Intersect(pred).Count();
        int fp = pred.Count - tp;
        int fn = gold.Count - tp;
        if (!_counts.TryGetValue(entityType, out var c)) c = (0, 0, 0);
        _counts[entityType] = (c.Tp + tp, c.Fp + fp, c.Fn + fn);
    }

    public IEnumerable<string> Types => _counts.Keys;

    public double F1For(string entityType)
    {
        if (!_counts.TryGetValue(entityType, out var c)) return double.NaN;
        return F1Metric.MicroF1(c.Tp, c.Fp, c.Fn);
    }

    public double MacroF1()
    {
        var vals = _counts.Keys.Select(F1For).Where(f => !double.IsNaN(f)).ToList();
        return vals.Count == 0 ? 0.0 : vals.Average();
    }
}

public record ComboResult(
    string TaskLM,
    string ReflectLM,
    PerTypeSpanCorpus Baseline,
    PerTypeSpanCorpus Optimized,
    string FinalInstruction,
    string? Error);

public record ModelChoice(string Slug, string DisplayName);