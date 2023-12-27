using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._22;

public abstract class Base : Solution
{
    public override string Description => "Sand slabs";
    
    protected List<(int Id, List<Point> Points)> Bricks = new();
    
    protected int SettleBricks(List<(int Id, List<Point> Points)> bricks, bool move = true)
    {
        var count = 0;

        foreach (var brick in bricks)
        {
            if (brick.Points[0].Z == 1)
            {
                continue;
            }

            var resting = Resting(bricks, brick.Points);

            if (! move && ! resting)
            {
                return 1;
            }

            if (! resting)
            {
                count++;

                while (! resting && brick.Points[0].Z > 1)
                {
                    foreach (var item in brick.Points)
                    {
                        item.Z--;
                    }

                    resting = Resting(bricks, brick.Points);
                }
            }
        }

        return count;
    }

    private bool Resting(List<(int Id, List<Point> Points)> bricks, List<Point> brick)
    {
        foreach (var item in bricks)
        {
            if (item.Points == brick)
            {
                continue;
            }

            if (item.Points[0].Z >= brick[0].Z || item.Points[0].Z < brick[0].Z - 5)
            {
                continue;
            }

            foreach (var left in item.Points)
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
        var id = 1;
        
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
            
            Bricks.Add((id, brick));

            id++;
        }

        Bricks = Bricks.OrderBy(b => b.Points[0].Z).ToList();
    }
}