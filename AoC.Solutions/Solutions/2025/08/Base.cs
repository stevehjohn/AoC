
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._08;

public abstract class Base : Solution
{
    public override string Description => "Playground";

    private readonly List<Edge> _edges = [];
    
    private Vertex[] _junctions;

    protected long GetAnswer(bool isPart2 = false)
    {
        ParseInput();
        
        CalculateDistances(isPart2);

        _edges.Sort();

        var disjointSet = new DisjointSet<Vertex>();

        foreach (var v in _junctions)
        {
            disjointSet.Add(v);
        }

        var components = _junctions.Length;
        
        var count = 0;
        
        var answer = 0L;

        foreach (var edge in _edges)
        {
            if (disjointSet.Union(edge.A, edge.B))
            {
                components--;

                if (components == 1)
                {
                    answer = (long) edge.A.X * edge.B.X;
                    break;
                }
            }

            count++;

            if (!isPart2 && count == 1_000)
            {
                var sizes = disjointSet.GetSizes().OrderDescending().ToArray();

                return (long) sizes[0] * sizes[1] * sizes[2];
            }
        }

        return answer;
    }

    private void CalculateDistances(bool isPart2)
    {
        _edges.Clear();

        if (isPart2)
        {
            for (var l = 0; l < _junctions.Length - 1; l++)
            {
                for (var r = l + 1; r < _junctions.Length; r++)
                {
                    _edges.Add(new Edge(_junctions[l], _junctions[r]));
                }
            }

            return;
        }

        const int limit = 1_000;

        var heap = new PriorityQueue<Edge, Edge>(Comparer<Edge>.Create((a, b) => b.CompareTo(a)));

        for (var l = 0; l < _junctions.Length - 1; l++)
        {
            for (var r = l + 1; r < _junctions.Length; r++)
            {
                var edge = new Edge(_junctions[l], _junctions[r]);

                if (heap.Count < limit)
                {
                    heap.Enqueue(edge, edge);
                }
                else if (edge.CompareTo(heap.Peek()) < 0)
                {
                    heap.DequeueEnqueue(edge, edge);
                }
            }
        }

        while (heap.TryDequeue(out var edge, out _))
        {
            _edges.Add(edge);
        }
    }

    private void ParseInput()
    {
        var i = 0;
        var size = Input.Length;

        _junctions = new Vertex[size];

        foreach (var line in Input)
        {
            _junctions[i++] = Vertex.Parse(line);
        }
    }
}
