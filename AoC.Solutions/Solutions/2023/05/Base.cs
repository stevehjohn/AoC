using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._05;

public abstract class Base : Solution
{
    public override string Description => "If you give a seed a fertilizer";

    protected long[] Seeds;

    private readonly List<List<(long Destination, long Source, long Range)>> _mappings = new();
    
    protected long RemapSeed(long seed)
    {
        foreach (var mapping in _mappings)
        {
            foreach (var map in mapping)
            {
                if (seed >= map.Source && seed < map.Source + map.Range)
                {
                    seed += map.Destination - map.Source;

                    break;
                }
            }
        }

        return seed;
    }

    protected void ParseInput()
    {
        Seeds = Input[0][6..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse).ToArray();

        var mapping = new List<(long Destination, long Source, long Range)>();

        foreach (var line in Input[3..])
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (!char.IsNumber(line[0]))
            {
                _mappings.Add(mapping);

                mapping = new List<(long Destination, long Source, long Range)>();

                continue;
            }

            var parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            mapping.Add((long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
        }

        _mappings.Add(mapping);
    }
}