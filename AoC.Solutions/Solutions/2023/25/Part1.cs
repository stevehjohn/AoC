using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._25;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<string, int> _vertices = new();

    private List<int>[] _links;

    private int[] _destinations;

    private int[] _capacity;

    private int[] _parent;

    private int[] _queue;

    public override string GetAnswer()
    {
        ParseInput();

        var count = _vertices.Count;

        _parent = new int[count];

        _queue = new int[count];

        const int source = 0;

        targets: for (var target = 1; target < count; target++)
        {
            Array.Fill(_capacity, 1);

            var flow = 0;
            
            int reachable;
            
            while (FindPath(source, target, out reachable))
            {
                if (++flow > 3)
                {
                    continue targets;
                }

                var vertex = target;

                while (vertex != source)
                {
                    var edge = _parent[vertex];

                    _capacity[edge]--;

                    _capacity[edge ^ 1]++;

                    vertex = _destinations[edge ^ 1];
                }
            }

            if (flow == 3)
            {
                return (reachable * (count - reachable)).ToString();
            }
        }

        throw new PuzzleException("Solution not found.");
    }

    private bool FindPath(int source, int target, out int reachable)
    {
        Array.Fill(_parent, -1);

        var head = 0;

        var tail = 0;

        _queue[tail++] = source;

        _parent[source] = -2;

        while (head < tail)
        {
            var vertex = _queue[head++];

            var links = _links[vertex];

            for (var i = 0; i < links.Count; i++)
            {
                var edge = links[i];

                if (_capacity[edge] == 0)
                {
                    continue;
                }

                var destination = _destinations[edge];

                if (_parent[destination] != -1)
                {
                    continue;
                }

                _parent[destination] = edge;

                if (destination == target)
                {
                    reachable = 0;

                    return true;
                }

                _queue[tail++] = destination;
            }
        }

        reachable = tail;

        return false;
    }

    private void ParseInput()
    {
        var edges = new HashSet<(int Left, int Right)>();

        foreach (var line in Input)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);

            var left = GetVertex(parts[0]);

            foreach (var connection in parts[1].Split(
                         ' ', StringSplitOptions.RemoveEmptyEntries))
            {
                var right = GetVertex(connection);

                edges.Add(left < right
                    ? (left, right)
                    : (right, left));
            }
        }

        _links = new List<int>[_vertices.Count];

        for (var i = 0; i < _links.Length; i++)
        {
            _links[i] = [];
        }

        _destinations = new int[edges.Count * 2];

        _capacity = new int[edges.Count * 2];

        var index = 0;

        foreach (var (left, right) in edges)
        {
            _links[left].Add(index);

            _destinations[index++] = right;

            _links[right].Add(index);

            _destinations[index++] = left;
        }
    }

    private int GetVertex(string name)
    {
        if (_vertices.TryGetValue(name, out var index))
        {
            return index;
        }

        index = _vertices.Count;

        _vertices.Add(name, index);

        return index;
    }
}