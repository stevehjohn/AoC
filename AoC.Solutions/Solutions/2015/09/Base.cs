using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._09;

public abstract class Base : Solution
{
    public override string Description => "All in a single night";

    private int[,] _distances;

    private int _count;

    protected void ParseInput()
    {
        var locations = new Dictionary<string, int>();

        var edges = new List<(int A, int B, int Distance)>();

        foreach (var line in Input)
        {
            var parts = line.Split(' ');

            var a = GetId(parts[0]);

            var b = GetId(parts[2]);

            edges.Add((a, b, int.Parse(parts[4])));
        }

        _count = locations.Count;

        _distances = new int[_count, _count];

        foreach (var (a, b, distance) in edges)
        {
            _distances[a, b] = distance;

            _distances[b, a] = distance;
        }

        int GetId(string name)
        {
            if (! locations.TryGetValue(name, out var id))
            {
                id = locations.Count;

                locations.Add(name, id);
            }

            return id;
        }
    }

    protected (int Shortet, int Longest) Solve()
    {
        var states = 1 << _count;

        var min = new int[states * _count];

        var max = new int[states * _count];

        Array.Fill(min, int.MaxValue);

        Array.Fill(max, int.MinValue);

        for (var i = 0; i < _count; i++)
        {
            var index = (1 << i) * _count + i;

            min[index] = 0;

            max[index] = 0;
        }

        for (var mask = 1; mask < states; mask++)
        {
            for (var last = 0; last < _count; last++)
            {
                var index = mask * _count + last;

                if (min[index] == int.MaxValue)
                {
                    continue;
                }

                for (var next = 0; next < _count; next++)
                {
                    var bit = 1 << next;

                    if ((mask & bit) != 0)
                    {
                        continue;
                    }

                    var destination =
                        (mask | bit) * _count + next;

                    var distance = _distances[last, next];

                    min[destination] = Math.Min(min[destination], min[index] + distance);

                    max[destination] = Math.Max(max[destination], max[index] + distance);
                }
            }
        }

        var shortest = int.MaxValue;

        var longest = int.MinValue;

        var offset = (states - 1) * _count;

        for (var i = 0; i < _count; i++)
        {
            shortest = Math.Min(shortest, min[offset + i]);

            longest = Math.Max(longest, max[offset + i]);
        }

        return (shortest, longest);
    }
}