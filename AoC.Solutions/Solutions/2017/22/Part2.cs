using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._22;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly HashSet<Point> _weakened = new();

    private readonly HashSet<Point> _flagged = new();

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

            _flagged.Add(Position);
        }
        else if (_flagged.Contains(Position))
        {
            Direction = new Point(-Direction.X, -Direction.Y);

            _flagged.Remove(Position);
        }
        else if (_weakened.Contains(Position))
        {
            _weakened.Remove(Position);

            Infected.Add(Position);

            infects = true;
        }
        else
        {
            Direction = new Point(Direction.Y, -Direction.X);

            _weakened.Add(new Point(Position));
        }

        Position.X += Direction.X;

        Position.Y += Direction.Y;

        return infects;
    }
}