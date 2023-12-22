using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._22;

public abstract class Base : Solution
{
    public override string Description => "Sand slabs";
    
    protected List<Brick> Bricks = new();
    
    protected bool SettleBricks(bool move = true)
    {
        bool moved;

        do
        {
            moved = false;
                
            foreach (var brick in Bricks)
            {
                if (brick.Points[0].Z == 1)
                {
                    continue;
                }

                var restsOn = Resting(brick);
                
                if (restsOn.Count == 0)
                {
                    if (move)
                    {
                        foreach (var item in brick.Points)
                        {
                            item.Z--;
                        }
                    }

                    moved = true;
                }
                else
                {
                    brick.RestsOn.AddRange(restsOn);
                    
                    restsOn.ForEach(b => b.Supports.Add(brick));
                }
            }

            if (moved && ! move)
            {
                return true;
            }
        } while (moved);

        return false;
    }

    private List<Brick> Resting(Brick brick)
    {
        var restsOn = new List<Brick>();
        
        foreach (var item in Bricks)
        {
            if (item == brick)
            {
                continue;
            }

            foreach (var left in item.Points)
            {
                foreach (var right in brick.Points)
                {
                    if (left.Z == right.Z - 1 && left.X == right.X && left.Y == right.Y)
                    {
                        restsOn.Add(item);
                    }
                }
            }
        }

        return restsOn;
    }

    protected void ParseInput()
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

            while (! start.Equals(end))
            {
                start = new Point(
                    start.X.Converge(end.X),
                    start.Y.Converge(end.Y),
                    start.Z.Converge(end.Z)
                );
                
                brick.Add(start);
            }
            
            Bricks.Add(new Brick { Points = brick });
        }

        Bricks = Bricks.OrderBy(b => b.Points[0].Z).ToList();
    }
}