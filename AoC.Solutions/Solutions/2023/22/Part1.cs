using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part1 : Base
{
    private List<(Point Start, Point End, Type Type)> _bricks = new();
    
    public override string GetAnswer()
    {
        ParseInput();
     
        SettleBricks();
        
        return "Unknown";
    }

    private void SettleBricks()
    {
        var moved = false;

        do
        {
            foreach (var brick in _bricks)
            {
                if (brick.Start.Z == 1)
                {
                    continue;
                }

                if (! Resting(brick))
                {
                    brick.Start.Z--;

                    brick.End.Z--;

                    moved = true;
                }
            }
        } while (moved);
    }

    private bool Resting((Point Start, Point End, Type Type) brick)
    {
        foreach (var item in _bricks)
        {
            if (item.Start.Z == brick.Start.Z - 1 || item.End.Z == brick.Start.Z - 1)
            {
            }
        }

        return false;
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

            Type type;
            
            if (start.Z != end.Z)
            {
                type = Type.Vertical;
            }
            else if (start.X != end.X)
            {
                type = Type.Horizontal;
            }
            else if (start.Y != end.Y)
            {
                type = Type.Deep;
            }
            else
            {
                type = Type.Square;
            }

            _bricks.Add((start, end, type));
        }

        _bricks = _bricks.OrderBy(b => b.Start.Z).ToList();
    }
}