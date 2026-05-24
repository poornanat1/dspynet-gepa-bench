// DataLoader.cs
using System.Globalization;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;
using DSpyNet.DSPy.Core;

namespace RealKIEBench;

public static class DataLoader
{
    public record Row(string Text, List<Span> Labels);
    public record Span(string Label, int Start, int End, string Text);

    public static List<Row> LoadCsv(string path, int max)
    {
        var rows = new List<Row>();
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });
        csv.Read(); csv.ReadHeader();
        while (csv.Read())
        {
            var text = csv.GetField<string>("text") ?? "";
            var labelsJson = csv.GetField<string>("labels") ?? "[]";
            List<Span> spans;
            try
            {
                using var doc = JsonDocument.Parse(labelsJson);
                spans = doc.RootElement.EnumerateArray().Select(e => new Span(
                    e.GetProperty("label").GetString() ?? "",
                    e.TryGetProperty("start", out var s) ? s.GetInt32() : 0,
                    e.TryGetProperty("end", out var en) ? en.GetInt32() : 0,
                    e.GetProperty("text").GetString() ?? ""
                )).ToList();
            }
            catch { spans = new List<Span>(); }

            if (text.Length > 8000) text = text.Substring(0, 8000); // cap tokens per doc
            rows.Add(new Row(text, spans));
            if (rows.Count >= max) break;
        }
        return rows;
    }

    /// <summary>Examples whose Extractions key holds the serialized gold spans for the F1 metric.</summary>
    public static List<Example> ToExamples(List<Row> rows, string entityTypeList)
    {
        return rows.Select(r =>
        {
            var gold = JsonSerializer.Serialize(r.Labels.Select(l => new { l.Label, l.Text }));
            return Example.From(("Passage", r.Text), ("EntityTypes", entityTypeList), ("Extractions", gold));
        }).ToList();
    }

    /// <summary>Union of label types seen across the rows; paper specifies 21, truncated dataset surfaces 24.</summary>
    public static List<string> DiscoverEntityTypes(IEnumerable<Row> rows)
    {
        var set = new HashSet<string>();
        foreach (var r in rows) foreach (var s in r.Labels) set.Add(s.Label);
        return set.OrderBy(x => x).ToList();
    }
}