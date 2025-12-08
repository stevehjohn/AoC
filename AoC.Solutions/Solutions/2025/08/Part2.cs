using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._08;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<Edge> _edges = [];

    private Vertex[] _junctions;

    public override string GetAnswer()
    {
        ParseInput();

        CalculateDistances();

        _edges.Sort();

        var disjointSet = new DisjointSet<Vertex>();

        foreach (var v in _junctions)
        {
            disjointSet.Add(v);
        }

        var components = _junctions.Length;

        long answer = 0;

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
        }

        return answer.ToString();
    }


    private void CalculateDistances()
    {
        for (var l = 0; l < _junctions.Length - 1; l++)
        {
            for (var r = l + 1; r < _junctions.Length; r++)
            {
                _edges.Add(new Edge(_junctions[l], _junctions[r]));
            }
        }
    }

    private void ParseInput()
    {
        var i = 0;

        var size = Input.Length;

        _junctions = new Vertex[size];

        foreach (var line in Input)
        {
            var vertex = Vertex.Parse(line);

            _junctions[i++] = vertex;
        }
    }
}