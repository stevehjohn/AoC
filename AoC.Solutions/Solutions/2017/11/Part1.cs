using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2017._11;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        var childPosition = WalkPath(Input[0]);

        return "TESTING";
    }

    private Point WalkPath(string path)
    {
        var steps = path.Split(',', StringSplitOptions.TrimEntries);

        var position = new Point();

        foreach (var step in steps)
        {
            var move = GetMovement(step);

            position.X += move.X;

            position.Y += move.Y;

            position.Z += move.Z;
        }

        return position;
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