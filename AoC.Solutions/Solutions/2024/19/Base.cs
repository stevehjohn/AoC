using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._19;

public abstract class Base : Solution
{
    public override string Description => "Linen layout";

    protected bool IsPart2;
    
    private string[] _towels;

    private string[] _designs;

    private readonly Dictionary<string, long> _cache = [];
    
    protected void ParseInput()
    {
        _towels = Input[0].Split(',', StringSplitOptions.TrimEntries);

        _designs = Input[2..];
    }

    protected long CheckEachTowel()
    {
        var count = 0L;

        for (var i = 0; i < _designs.Length; i++)
        {
            var result = CountPossibilities(_designs[i]);

            count += IsPart2 ? result : result > 0 ? 1 : 0;
        }

        return count;
    }

    private long CountPossibilities(string design)
    {
        if (_cache.TryGetValue(design, out var value))
        {
            return value;
        }

        var count = _towels.Contains(design) ? 1L : 0L;
        
        for (var i = 0; i < _towels.Length; i++)
        {
            var towel = _towels[i];

            if (design.StartsWith(towel))
            {
                count += CountPossibilities(design[towel.Length..]);
            }
        }

        _cache[design] = count;

        return count;
    }
}