using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._24;

public abstract class Base : Solution
{
    public override string Description => "Tile flipping";

    private readonly List<List<string>> _instructions = new();

    protected readonly HashSet<Point> BlackTiles = new();

    protected void FlipTiles()
    {
        foreach (var instruction in _instructions)
        {
            var position = new Point();

            foreach (var direction in instruction)
            {
                var movement = GetMovement(direction);

                position.X += movement.X;

                position.Y += movement.Y;
             
                position.Z += movement.Z;
            }

            if (! BlackTiles.Add(position))
            {
                BlackTiles.Remove(position);
            }
        }
    }

    private static Point GetMovement(string direction)
    {
        return direction switch
        {
            "e" => new Point(1, 0, -1),
            "w" => new Point(-1, 0, 1),
            "ne" => new Point(1, -1),
            "se" => new Point(0, 1, -1),
            "nw" => new Point(0, -1, 1),
            "sw" => new Point(-1, 1),
            _ => throw new PuzzleException("Direction not recognised")
        };
    }

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var directions = new List<string>();

            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == 's' || line[i] == 'n')
                {
                    directions.Add($"{line[i]}{line[i + 1]}");

                    i++;

                    continue;
                }

                directions.Add(line[i].ToString());
            }

            _instructions.Add(directions);
        }
    }
}