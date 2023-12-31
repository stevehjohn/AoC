using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._22;

[UsedImplicitly]
public class Part2 : Base
{
    protected readonly HashSet<Point> Weakened = new();
    
    protected readonly HashSet<Point> Flagged = new();

    public override string GetAnswer()
    {
        ParseInput();

        var infections = 0;

        for (var i = 0; i < 10_000_000; i++)
        {
            infections += RunCycle() ? 1 : 0;
        }

        return infections.ToString();
    }

    private bool RunCycle()
    {
        var infects = false;

        if (Infected.Contains(Position))
        {
            Direction = new Point(-Direction.Y, Direction.X);

            Infected.Remove(Position);

            Flagged.Add(Position);
        }
        else if (Flagged.Contains(Position))
        {
            Direction = new Point(-Direction.X, -Direction.Y);

            Flagged.Remove(Position);
        }
        else if (Weakened.Contains(Position))
        {
            Weakened.Remove(Position);

            Infected.Add(Position);

            infects = true;
        }
        else
        {
            Direction = new Point(Direction.Y, -Direction.X);

            Weakened.Add(new Point(Position));
        }

        Position.X += Direction.X;

        Position.Y += Direction.Y;

        return infects;
    }
}