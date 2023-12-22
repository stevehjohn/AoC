using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part1 : Base
{
    private List<List<Point>> _bricks = new();
    
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
                if (brick[0].Z == 1)
                {
                    continue;
                }

                if (! Resting(brick))
                {
                    foreach (var item in brick)
                    {
                        item.Z--;
                    }
                    
                    moved = true;
                }
            }
        } while (moved);
    }

    private bool Resting(List<Point> brick)
    {
        foreach (var item in _bricks)
        {
            foreach (var left in item)
            {
                foreach (var right in brick)
                {
                    if (left.Z == right.Z - 1 && left.X == right.X && left.Y == right.Y)
                    {
                        return true;
                    }
                }
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

            var brick = new List<Point> { start };

            while (start.X != end.X && start.Y != end.Y && start.Z != end.Z)
            {
                start.X = start.X.Converge(end.X);
                start.Y = start.Y.Converge(end.Y);
                start.Z = start.Z.Converge(end.Z);
            }

            _bricks.Add(brick);
        }

        _bricks = _bricks.OrderBy(b => b[0].Z).ToList();
    }
}