// F1Metric.cs
using System.Text.Json;
using DSpyNet.DSPy.Core;

namespace RealKIEBench;

public static class F1Metric
{
    /// <summary>Macro-F1 across entity types; feedback string surfaces worst-offending types and concrete misses.</summary>
    public static FeedbackMetric Build(List<string> entityTypes) => (gold, pred, predName) =>
    {
        var (g, p) = ExtractSpans(gold, pred, entityTypes);
        double sumF1 = 0; int counted = 0;
        var perTypeF1 = new Dictionary<string, double>();
        foreach (var t in entityTypes)
        {
            if (g[t].Count == 0 && p[t].Count == 0) continue;
            double f1 = F1FromSets(g[t], p[t]);
            perTypeF1[t] = f1;
            sumF1 += f1; counted++;
        }
        double macro = counted == 0 ? 0 : sumF1 / counted;

        var worst = perTypeF1.OrderBy(kv => kv.Value).Take(3)
            .Select(kv => $"{kv.Key}={kv.Value:F2}").ToList();
        var fb = $"Macro F1={macro:F3}. Worst types: {string.Join(", ", worst)}. " +
                 BuildPredictorHint(p, g);
        return new ScoreFeedback(macro, fb);
    };

    /// <summary>{type → (gold, pred)} sets for one example; the benchmark aggregates across docs for micro-F1.</summary>
    public static Dictionary<string, (HashSet<string> Gold, HashSet<string> Pred)>
        ExtractPerExampleSpans(Example gold, Prediction pred, List<string> entityTypes)
    {
        var (g, p) = ExtractSpans(gold, pred, entityTypes);
        return entityTypes.ToDictionary(t => t, t => (g[t], p[t]));
    }

    public static double F1FromSets(HashSet<string> gold, HashSet<string> pred)
    {
        if (gold.Count == 0 && pred.Count == 0) return 1.0;
        int tp = gold.Intersect(pred).Count();
        double prec = pred.Count == 0 ? 0 : (double)tp / pred.Count;
        double rec  = gold.Count == 0 ? 0 : (double)tp / gold.Count;
        return (prec + rec) == 0 ? 0 : 2 * prec * rec / (prec + rec);
    }

    /// <summary>Micro-F1 from accumulated TP/FP/FN counts across a corpus.</summary>
    public static double MicroF1(int tp, int fp, int fn)
    {
        if (tp == 0 && fp == 0 && fn == 0) return double.NaN;
        double prec = tp + fp == 0 ? 0 : (double)tp / (tp + fp);
        double rec  = tp + fn == 0 ? 0 : (double)tp / (tp + fn);
        return (prec + rec) == 0 ? 0 : 2 * prec * rec / (prec + rec);
    }

    private static (Dictionary<string, HashSet<string>> Gold, Dictionary<string, HashSet<string>> Pred)
        ExtractSpans(Example gold, Prediction pred, List<string> types)
    {
        var goldJson = gold.Get<string>("Extractions");
        var predJson = pred.Get<string>("Extractions");
        return (ParseGoldSpans(goldJson, types), ParsePredObject(predJson, types));
    }

    private static string BuildPredictorHint(Dictionary<string, HashSet<string>> p, Dictionary<string, HashSet<string>> g)
    {
        var lines = new List<string>();
        foreach (var t in g.Keys)
        {
            var missed = g[t].Except(p[t]).Take(2).ToList();
            var spurious = p[t].Except(g[t]).Take(2).ToList();
            if (missed.Count > 0) lines.Add($"Missed {t}: [{string.Join(", ", missed)}]");
            if (spurious.Count > 0) lines.Add($"Spurious {t}: [{string.Join(", ", spurious)}]");
            if (lines.Count >= 4) break;
        }
        return lines.Count == 0 ? "" : " | " + string.Join("; ", lines);
    }

    private static Dictionary<string, HashSet<string>> ParseGoldSpans(string goldJson, List<string> types)
    {
        var d = types.ToDictionary(t => t, _ => new HashSet<string>(StringComparer.OrdinalIgnoreCase));
        if (string.IsNullOrWhiteSpace(goldJson)) return d;
        try
        {
            using var doc = JsonDocument.Parse(goldJson);
            foreach (var el in doc.RootElement.EnumerateArray())
            {
                var label = el.GetProperty("Label").GetString() ?? "";
                var text = (el.GetProperty("Text").GetString() ?? "").Trim();
                if (!d.ContainsKey(label) || string.IsNullOrWhiteSpace(text)) continue;
                d[label].Add(text);
            }
        }
        catch { /* malformed gold = empty sets */ }
        return d;
    }

    private static Dictionary<string, HashSet<string>> ParsePredObject(string predJson, List<string> types)
    {
        var d = types.ToDictionary(t => t, _ => new HashSet<string>(StringComparer.OrdinalIgnoreCase));
        if (string.IsNullOrWhiteSpace(predJson)) return d;

        // Recover the JSON if the LM wrapped it in fences or added chatter.
        string trimmed = predJson.Trim();
        int firstBrace = trimmed.IndexOf('{');
        int lastBrace = trimmed.LastIndexOf('}');
        if (firstBrace < 0 || lastBrace <= firstBrace) return d;
        trimmed = trimmed.Substring(firstBrace, lastBrace - firstBrace + 1);

        try
        {
            using var doc = JsonDocument.Parse(trimmed);
            foreach (var kv in doc.RootElement.EnumerateObject())
            {
                var key = kv.Name;
                if (!d.ContainsKey(key)) continue;
                if (kv.Value.ValueKind != JsonValueKind.Array) continue;
                foreach (var item in kv.Value.EnumerateArray())
                {
                    string? s = item.ValueKind switch
                    {
                        JsonValueKind.String => item.GetString(),
                        JsonValueKind.Number => item.GetRawText(),
                        _ => null
                    };
                    if (!string.IsNullOrWhiteSpace(s)) d[key].Add(s.Trim());
                }
            }
        }
        catch { /* malformed prediction = empty sets, scored as 0 */ }
        return d;
    }
}