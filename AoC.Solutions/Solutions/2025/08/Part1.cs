using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._08;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<Vertex> Junctions = [];

    private readonly List<Edge> Edges = [];
    
    public override string GetAnswer()
    {
        ParseInput();

        CalculateDistances();
        
        Edges.Sort();

        return "Unknown";
    }

    private void CalculateDistances()
    {
        for (var l = 0; l < Junctions.Count - 1; l++)
        {
            for (var r = l + 1; r < Junctions.Count; r++)
            {
                Edges.Add(new Edge(Junctions[l], Junctions[r]));
            }
        }
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            Junctions.Add(Vertex.Parse(line));
        }
    }
}