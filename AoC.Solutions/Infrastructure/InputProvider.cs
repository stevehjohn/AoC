using System.Diagnostics.CodeAnalysis;

namespace AoC.Solutions.Infrastructure;

[ExcludeFromCodeCoverage]
public static class InputProvider
{
    public static string[] GetInput(string nameSpace)
    {
        var parts = nameSpace.Split('.');

        var pathParts = parts.Skip(2).Select(s => s.Replace("_", string.Empty)).ToArray();

        string[] input;

        string path;
        
        if (! Path.Exists("./Solutions"))
        {
            path = $"./Aoc.Solutions/{string.Join(Path.DirectorySeparatorChar, pathParts)}{Path.DirectorySeparatorChar}";
            
            if (! File.Exists($"{path}input.clear") && ! File.Exists($"{path}input.encrypted"))
            {
                DownloadInput(path);
            }
            
            input = CryptoFileProvider.LoadFile(path, "input.clear");
        }
        else
        {
            path = $"./{string.Join(Path.DirectorySeparatorChar, pathParts)}{Path.DirectorySeparatorChar}";
            
            if (! File.Exists($"{path}input.clear") && ! File.Exists($"{path}input.encrypted"))
            {
                DownloadInput(path);
            }
            
            input = CryptoFileProvider.LoadFile(path, "input.clear");
        }

        return input;
    }

    private static void DownloadInput(string path)
    {
        var parts = path.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);

        var url = $"https://adventofcode.com/{parts[^2]}/day/{int.Parse(parts[^1])}/input";
        
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        
        var keyData = File.ReadLines(GetKeyPath()).Select(l => l.Split(":", StringSplitOptions.TrimEntries)[1]).ToArray();

        request.Headers.Add("Cookie", $"session={keyData[1]}");

        using var client = new HttpClient();

        using var response = client.Send(request);

        if (! response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Status code {response.StatusCode} received calling {url} to retrieve puzzle input.");
            
            Console.WriteLine($"Session cookie {keyData[1]} used.");
            
            return;
        }

        var input = response.Content.ReadAsStringAsync().Result;
        
        File.WriteAllText($"{path}input.clear", input);
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