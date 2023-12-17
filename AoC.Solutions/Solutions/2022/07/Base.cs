using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._07;

public abstract class Base : Solution
{
    public override string Description => "No space left on device";

    protected readonly Dictionary<string, long> DirectorySizes = new();

    protected void ProcessCommands()
    {
        var level = new Stack<string>();

        foreach (var line in Input.Skip(1))
        {
            if (line[0] == '$')
            {
                if (line[2..4] == "cd")
                {
                    if (line[5] == '.')
                    {
                        level.Pop();
                    }
                    else
                    {
                        level.Push(line[5..]);
                    }
                }

                continue;
            }

            if (line.StartsWith("dir"))
            {
                continue;
            }

            var path = level.ToArray().Reverse().ToList();

            var i = path.Count;

            while (i >= 0)
            {
                var key = $"/{string.Join("/", path.Take(i))}";

                var value = long.Parse(line.Split(' ')[0]);

                if (! DirectorySizes.TryAdd(key, value))
                {
                    DirectorySizes[key] += value;
                }

                i--;
            }
        }
    }
}
