using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part1 : Base
{
    private List<(Point Start, Point End)> _bricks = new();
    
    public override string GetAnswer()
    {
        ParseInput();
     
        SettleBricks();
        
        return "Unknown";
    }

    private void SettleBricks()
    {
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split('~');

            var start = Point.Parse(parts[0]);

            var end = Point.Parse(parts[1]);

            if (end.Z < start.Z)
            {
                (start, end) = (end, start);
            }

            _bricks.Add((start, end));
        }

        _bricks = _bricks.OrderBy(b => b.Start.Z).ToList();
    }
}