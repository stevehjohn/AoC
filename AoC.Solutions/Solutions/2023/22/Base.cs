using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._22;

public abstract class Base : Solution
{
    public override string Description => "Sand slabs";
    
    protected List<(char C, List<Point> P)> Bricks = new();
    
    protected int CountSupportingBricks()
    {
        var count = 0;

        var settledState = Bricks.ToList();

        foreach (var brick in settledState)
        {
            Bricks.Remove(brick);

            count += SettleBricks(false) ? 1 : 0;

            Bricks.Add(brick);
        }
        
        return count;
    }
    
    protected bool SettleBricks(bool move = true)
    {
        bool moved;

        var count = 0;
        
        do
        {
            moved = false;
                
            foreach (var brick in Bricks)
            {
                if (brick.P[0].Z == 1)
                {
                    continue;
                }

                if (! Resting(brick.P))
                {
                    if (move)
                    {
                        foreach (var item in brick.P)
                        {
                            item.Z--;
                        }
                    }

                    moved = true;

                    count++;
                }
            }

            if (count > 0 && ! move)
            {
                return true;
            }
        } while (moved);

        return count > 0;
    }

    private bool Resting(List<Point> brick)
    {
        foreach (var item in Bricks)
        {
            if (item.P == brick)
            {
                continue;
            }

            foreach (var left in item.P)
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

    protected void ParseInput()
    {
        var id = 'A';
        
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

            while (! start.Equals(end))
            {
                start = new Point(
                    start.X.Converge(end.X),
                    start.Y.Converge(end.Y),
                    start.Z.Converge(end.Z)
                );
                
                brick.Add(start);
            }
            
            Bricks.Add((id, brick.OrderBy(p => p.Z).ToList()));

            id++;
        }

        Bricks = Bricks.OrderBy(b => b.P[0].Z).ToList();
    }
}