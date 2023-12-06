using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._05;

[UsedImplicitly]
public class Part1 : Base
{
    private long[] _seeds;

    private readonly List<List<(long Destination, long Source, long Range)>> _mappings = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < _seeds.Length; i++)
        {
            _seeds[i] = RemapSeed(_seeds[i]);
        }
        
        return _seeds.Min().ToString();
    }

    private long RemapSeed(long seed)
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

    private void ParseInput()
    {
        _seeds = Input[0][6..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

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