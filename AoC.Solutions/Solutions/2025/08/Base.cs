using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._08;

public abstract class Base : Solution
{
    public override string Description => "Playground";

    protected readonly List<Edge> Edges = [];

    protected Vertex[] Junctions;

    protected void CalculateDistances()
    {
        for (var l = 0; l < Junctions.Length - 1; l++)
        {
            for (var r = l + 1; r < Junctions.Length; r++)
            {
                Edges.Add(new Edge(Junctions[l], Junctions[r]));
            }
        }
    }

    protected void ParseInput()
    {
        var i = 0;

        var size = Input.Length;

        Junctions = new Vertex[size];

        foreach (var line in Input)
        {
            var vertex = Vertex.Parse(line);

            Junctions[i++] = vertex;
        }
    }
}