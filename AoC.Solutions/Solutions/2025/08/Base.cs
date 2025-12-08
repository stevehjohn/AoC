using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._08;

public abstract class Base : Solution
{
    public override string Description => "Puzzle 08";

    private readonly List<Point> _boxes = [];
    
    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            _boxes.Add(Point.Parse(line));
        }
    }
}