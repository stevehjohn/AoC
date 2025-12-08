using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._08;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<Vertex> _junctions = [];

    private readonly List<Edge> _edges = [];
    
    public override string GetAnswer()
    {
        ParseInput();

        CalculateDistances();
        
        _edges.Sort();

        return "Unknown";
    }

    private void CalculateDistances()
    {
        for (var l = 0; l < _junctions.Count - 1; l++)
        {
            for (var r = l + 1; r < _junctions.Count; r++)
            {
                _edges.Add(new Edge(_junctions[l], _junctions[r]));
            }
        }
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            _junctions.Add(Vertex.Parse(line));
        }
    }
}