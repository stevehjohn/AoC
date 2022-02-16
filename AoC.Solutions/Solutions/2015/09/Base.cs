using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._09;

public abstract class Base : Solution
{
    public override string Description => "All in a single night";

    private string[] _locations;

    private readonly Dictionary<string, int> _distances = new();

    protected (int Shortet, int Longest) Solve()
    {
        var permutations = _locations.GetPermutations();

        var shortest = int.MaxValue;

        var longest = int.MinValue;

        foreach (var permutation in permutations)
        {
            var length = 0;

            for (var i = 0; i < permutation.Length - 1; i++)
            {
                length += _distances[$"{permutation[i]}{permutation[i + 1]}"];
            }

            if (length < shortest)
            {
                shortest = length;
            }

            if (length > longest)
            {
                longest = length;
            }
        }

        return (shortest, longest);
    }

    protected void ParseInput()
    {
        _locations = Input.Select(l => l.Split(' ', StringSplitOptions.TrimEntries)[0]).Union(Input.Select(l => l.Split(' ', StringSplitOptions.TrimEntries)[2])).Distinct().ToArray();

        foreach (var line in Input)
        {
            var parts = line.Split(" = ", StringSplitOptions.TrimEntries);

            var route = parts[0].Split(" to ", StringSplitOptions.TrimEntries);

            _distances.Add($"{route[0]}{route[1]}", int.Parse(parts[1]));

            if (! _distances.ContainsKey($"{route[1]}{route[0]}"))
            {
                _distances.Add($"{route[1]}{route[0]}", int.Parse(parts[1]));
            }
        }
    }
}