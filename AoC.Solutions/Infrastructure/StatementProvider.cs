namespace AoC.Solutions.Infrastructure;

public static class StatementProvider
{
    public static string[] GetStatement(string nameSpace)
    {
        var parts = nameSpace.Split('.');

        var pathParts = parts.Skip(2).Select(s => s.Replace("_", string.Empty)).ToArray();

        string[] input;

        string path;
        
        if (! Path.Exists("./Solutions"))
        {
            path = $"./Aoc.Solutions/{string.Join(Path.DirectorySeparatorChar, pathParts)}{Path.DirectorySeparatorChar}";
            
            if (! File.Exists($"{path}statement.md") && ! File.Exists($"{path}statement.encrypted"))
            {
                DownloadStatement(path);
            }
            
            input = CryptoFileProvider.LoadFile(path, "statement.md");
        }
        else
        {
            path = $"./{string.Join(Path.DirectorySeparatorChar, pathParts)}{Path.DirectorySeparatorChar}";
            
            if (! File.Exists($"{path}statement.md") && ! File.Exists($"{path}statement.encrypted"))
            {
                DownloadStatement(path);
            }
            
            input = CryptoFileProvider.LoadFile(path, "statement.md");
        }

        return input;
    }
    
    private static void DownloadStatement(string path)
    {
        var parts = path.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);
        
        using var request = new HttpRequestMessage(HttpMethod.Get, $"https://adventofcode.com/{parts[^2]}/day/{parts[^1]}");
        
        var keyData = File.ReadLines(GetKeyPath()).Select(l => l.Split(":", StringSplitOptions.TrimEntries)[1]).ToArray();

        request.Headers.Add("Cookie", $"session={keyData[3]}");

        using var client = new HttpClient();

        using var response = client.Send(request);

        var input = response.Content.ReadAsStringAsync().Result;
        
        File.WriteAllText($"{path}statement.md", input);
    }

    private static string GetKeyPath()
    {
        if (File.Exists("./AoC.Key"))
        {
            return "./AoC.Key";
        }

        if (File.Exists("./AoC.Solutions/AoC.Key"))
        {
            return "./AoC.Solutions/AoC.Key";
        }

        return null;
    }
}