// ResultsWriter.cs
using System.Globalization;
using System.Text;

namespace RealKIEBench;

public static class ResultsWriter
{
    public static string Render(
        List<ComboResult> results,
        List<string> entityTypes,
        int trainCount,
        int testCount,
        DateTime when)
    {
        var sb = new StringBuilder();
        sb.AppendLine("# RealKIE SEC S-1 — GEPA Model Combo Benchmark");
        sb.AppendLine();
        sb.AppendLine($"_Generated {when:yyyy-MM-dd HH:mm} UTC_");
        sb.AppendLine();
        sb.AppendLine("## Setup");
        sb.AppendLine();
        sb.AppendLine($"- Dataset: RealKIE `s1_truncated`, [Indico](https://indicodatasolutions.github.io/RealKIE/)");
        sb.AppendLine($"- Train passages: **{trainCount}** | Test passages: **{testCount}**");
        sb.AppendLine($"- Entity types: **{entityTypes.Count}**");
        sb.AppendLine($"- Metric: per-type micro-F1, then macro-averaged across types");
        sb.AppendLine($"- GEPA budget: `Auto = Light`, reflection minibatch = 3, val split = 0.3, seed = 42");
        sb.AppendLine();

        sb.AppendLine("## Macro F1 Summary");
        sb.AppendLine();
        sb.AppendLine("| Task LM | Reflection LM | Baseline | GEPA | Δ (pp) | Notes |");
        sb.AppendLine("|---|---|---:|---:|---:|---|");
        foreach (var r in results)
        {
            double bl = r.Baseline.MacroF1();
            double opt = r.Optimized.MacroF1();
            double delta = (opt - bl) * 100;
            var notes = r.Error == null ? "" : $"⚠ {r.Error}";
            sb.AppendLine($"| {r.TaskLM} | {r.ReflectLM} | {Fmt(bl)} | {Fmt(opt)} | {DeltaFmt(delta)} | {notes} |");
        }
        sb.AppendLine();

        foreach (var taskGroup in results.GroupBy(r => r.TaskLM).OrderBy(g => g.Key))
        {
            sb.AppendLine($"## Task LM = {taskGroup.Key}");
            sb.AppendLine();
            var reflectCombos = taskGroup.OrderBy(c => c.ReflectLM).ToList();
            var baselineCorpus = reflectCombos.First().Baseline;

            sb.AppendLine($"Baseline macro F1: **{Fmt(baselineCorpus.MacroF1())}**");
            sb.AppendLine();
            sb.AppendLine("Per-entity-type F1 (and Δ vs baseline in pp):");
            sb.AppendLine();

            sb.Append("| Entity type | Baseline ");
            foreach (var rc in reflectCombos) sb.Append($"| reflect=`{rc.ReflectLM}` ");
            sb.AppendLine("|");
            sb.Append("|---|---:");
            foreach (var _ in reflectCombos) sb.Append("|---:");
            sb.AppendLine("|");

            foreach (var t in entityTypes.OrderBy(x => x))
            {
                double bl = baselineCorpus.F1For(t);
                sb.Append($"| {Escape(t)} | {Fmt(bl)} ");
                foreach (var rc in reflectCombos)
                {
                    double opt = rc.Optimized.F1For(t);
                    double delta = (opt - bl) * 100;
                    sb.Append($"| {Fmt(opt)} ({DeltaFmt(delta)}) ");
                }
                sb.AppendLine("|");
            }
            sb.AppendLine();
        }

        sb.AppendLine("## Final Instructions Produced by GEPA");
        sb.AppendLine();
        foreach (var r in results.Where(x => !string.IsNullOrWhiteSpace(x.FinalInstruction)))
        {
            sb.AppendLine($"### task={r.TaskLM} · reflect={r.ReflectLM}");
            sb.AppendLine();
            sb.AppendLine("```text");
            sb.AppendLine(r.FinalInstruction);
            sb.AppendLine("```");
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private static string Fmt(double v) => double.IsNaN(v) ? "—" : v.ToString("F3", CultureInfo.InvariantCulture);
    private static string DeltaFmt(double pp) => double.IsNaN(pp) ? "—" : pp.ToString("+0.0;-0.0;0.0", CultureInfo.InvariantCulture);
    private static string Escape(string s) => s.Replace("|", "\\|");
}