using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._11;

public abstract class Base : Solution
{
    public override string Description => "Hex ed";

    protected static (Point Position, int Furthest) WalkPath(string path)
    {
        var steps = path.Split(',', StringSplitOptions.TrimEntries);

        var position = new Point();

        var furthest = 0;

        foreach (var step in steps)
        {
            var move = GetMovement(step);

            position.X += move.X;

            position.Y += move.Y;

            position.Z += move.Z;

            var distance = (Math.Abs(position.X) + Math.Abs(position.Y) + Math.Abs(position.Z)) / 2;

            if (distance > furthest)
            {
                furthest = distance;
            }
        }

        return (position, furthest);
    }

    private static Point GetMovement(string direction)
    {
        return direction switch
        {
            "n" => new Point(0, -1, 1),
            "s" => new Point(0, 1, -1),
            "ne" => new Point(1, -1),
            "se" => new Point(1, 0, -1),
            "nw" => new Point(-1, 0, 1),
            "sw" => new Point(-1, 1),
            _ => throw new PuzzleException("Direction not recognised")
        };
    }
}