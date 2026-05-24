// S1ExtractionSig.cs — extraction signature for SEC S-1 filings.
using DSpyNet.DSPy.Core;

namespace RealKIEBench;

[DspInstruction(
    "Extract key information from the SEC S-1 filing passage. " +
    "For each entity type listed, find every span in the passage whose text fits the type and report it. " +
    "Return a JSON object whose keys are the entity type names and whose values are arrays of the exact extracted strings. " +
    "If no spans of a type are present, return an empty array for that key. " +
    "Output only the JSON object — no commentary, no markdown fences.")]
public class S1ExtractionSig : IDSpySignature
{
    [DspInput(Prefix = "Passage:", Description = "A passage from an SEC S-1 filing")]
    public string Passage { get; set; } = "";

    [DspInput(Prefix = "Entity types:", Description = "Comma-separated list of entity types to extract")]
    public string EntityTypes { get; set; } = "";

    [DspOutput(Prefix = "Extractions:", Description = "JSON object mapping entity type -> list of extracted strings")]
    public string Extractions { get; set; } = "";
}