using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._08;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<Edge> _edges = [];
    
    private Vertex[] _junctions;

    public override string GetAnswer()
    {
        ParseInput();

        CalculateDistances();
        
        _edges.Sort();

        var connections = 0;

        var disjointSet = new DisjointSet<Vertex>();
        
        foreach (var v in _junctions)
        {
            disjointSet.Add(v);
        }
        
        foreach (var edge in _edges)
        {
            disjointSet.Union(edge.A, edge.B);

            connections++;

            if (connections == 1_000)
            {
                break;
            }
        }

        var sizes = disjointSet.GetSizes().OrderDescending().ToArray();

        return (sizes[0] * sizes[1] * sizes[2]).ToString();
    }
}