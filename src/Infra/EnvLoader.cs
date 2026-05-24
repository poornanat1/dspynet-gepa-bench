// EnvLoader.cs
namespace RealKIEBench;

public static class EnvLoader
{
    /// <summary>Reads a value from the OS environment, falling back to a .env file at the project root.</summary>
    public static string? Get(string key)
    {
        var v = Environment.GetEnvironmentVariable(key);
        if (!string.IsNullOrWhiteSpace(v)) return v;

        // bin/Debug/net8.0 -> project root is three levels up.
        var envPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".env");
        if (!File.Exists(envPath)) return null;

        foreach (var line in File.ReadAllLines(envPath))
        {
            var trimmed = line.Trim();
            if (trimmed.Length == 0 || trimmed.StartsWith("#")) continue;
            var eq = trimmed.IndexOf('=');
            if (eq <= 0) continue;
            var k = trimmed[..eq].Trim();
            if (k != key) continue;
            return trimmed[(eq + 1)..].Trim().Trim('"');
        }
        return null;
    }
}