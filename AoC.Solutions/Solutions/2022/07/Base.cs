using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._07;

public abstract class Base : Solution
{
    public override string Description => "No Space Left On Device";

    private readonly Dictionary<string, long> _fileSizes = new();

    protected readonly Dictionary<string, long> DirectorySizes = new();

    protected void ProcessCommands()
    {
        var location = "/";

        foreach (var line in Input)
        {
            if (line[..4] == "$ cd")
            {
                if (line[5] == '/')
                {
                    location = "/";
                }
                else if (line.Length > 6 && line[5..7] == "..")
                {
                    if (location == "/")
                    {
                        continue;
                    }

                    var dirs = location.Split('/', StringSplitOptions.RemoveEmptyEntries);

                    location = $"/{string.Join('/', dirs[..^1])}";
                }
                else
                {
                    location = $"{location}{line[5..]}/";
                }

                continue;
            }

            if (line == "$ ls" || line[..3] == "dir")
            {
                continue;
            }

            var parts = line.Split(' ');

            var size = int.Parse(parts[0]);

            _fileSizes.TryAdd($"{location}{parts[1]}", size);
        }
    }

    protected void CalculateDirectorySizes()
    {
        foreach (var file in _fileSizes)
        {
            var dir = file.Key.Substring(0, file.Key.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(dir))
            {
                dir = "/";
            }

            if (DirectorySizes.ContainsKey(dir))
            {
                DirectorySizes[dir] += file.Value;
            }
            else
            {
                DirectorySizes.Add(dir, file.Value);
            }
        }
    }

    protected void SumDirectorySizes()
    {
        for (var i = 0; i < DirectorySizes.Count; i++)
        {
            var dir = DirectorySizes.ElementAt(i);

            if (dir.Key == "/")
            {
                continue;
            }

            var up = dir.Key;

            while (true)
            {
                up = up[..up.LastIndexOf('/')];

                if (string.IsNullOrWhiteSpace(up))
                {
                    up = "/";
                }

                if (DirectorySizes.ContainsKey(up))
                {
                    DirectorySizes[up] += dir.Value;
                }
                else
                {
                    DirectorySizes.Add(up, dir.Value);
                }

                if (up == "/")
                {
                    break;
                }
            }
        }
    }
}