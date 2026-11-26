using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._12;

public abstract class Base : Solution
{
    public override string Description => "Caving";

    private List<int>[] _links;

    private ulong[] _bits;

    private int _start;

    private int _end;

    private readonly Dictionary<(int, ulong, bool), long> _cache = new();

    protected long Solve(bool allowDoubleVisit)
    {
        LoadMap();

        _cache.Clear();

        return Visit(_start, 0, ! allowDoubleVisit);
    }

    private long Visit(int node, ulong visited, bool usedDouble)
    {
        if (node == _end)
        {
            return 1;
        }

        var key = (node, visited, usedDouble);

        if (_cache.TryGetValue(key, out var result))
        {
            return result;
        }

        var count = 0L;

        foreach (var next in _links[node])
        {
            if (next == _start)
            {
                continue;
            }

            var bit = _bits[next];

            if ((visited & bit) == 0)
            {
                count += Visit(next, visited | bit, usedDouble);
            }
            else if (! usedDouble)
            {
                count += Visit(next, visited, true);
            }
        }

        _cache[key] = count;

        return count;
    }

    private void LoadMap()
    {
        var ids = new Dictionary<string, int>();

        var edges = new List<(int A, int B)>();

        foreach (var line in Input)
        {
            var parts = line.Split('-');

            edges.Add((GetId(parts[0]), GetId(parts[1])));
        }

        _start = ids["start"];

        _end = ids["end"];

        _links = new List<int>[ids.Count];

        _bits = new ulong[ids.Count];

        for (var i = 0; i < ids.Count; i++)
        {
            _links[i] = [];
        }

        var bitIndex = 0;

        foreach (var (name, id) in ids)
        {
            if (id != _start && id != _end && char.IsLower(name[0]))
            {
                if (bitIndex == 64)
                {
                    throw new InvalidOperationException("Too many small caves.");
                }

                _bits[id] = 1UL << bitIndex++;
            }
        }

        foreach (var (a, b) in edges)
        {
            _links[a].Add(b);

            _links[b].Add(a);
        }

        return;

        int GetId(string name)
        {
            if (! ids.TryGetValue(name, out var id))
            {
                id = ids.Count;

                ids.Add(name, id);
            }

            return id;
        }
    }
}