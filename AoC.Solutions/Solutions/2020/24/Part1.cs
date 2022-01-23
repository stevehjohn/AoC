using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._24;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<List<string>> _instructions = new();

    private readonly HashSet<Point> _blackTiles = new();

    public override string GetAnswer()
    {
        ParseInput();

        FlipTiles();

        return _blackTiles.Count.ToString();
    }

    private void FlipTiles()
    {
        foreach (var instruction in _instructions)
        {
            var position = new Point();

            foreach (var direction in instruction)
            {
                var movement = GetMovement(direction);

                position.X += movement.X;

                position.Y += movement.Y;
            }

            if (_blackTiles.Contains(position))
            {
                _blackTiles.Remove(position);
            }
            else
            {
                _blackTiles.Add(position);
            }
        }
    }

    private static Point GetMovement(string direction)
    {
        return direction switch
        {
            "e" => new Point(2, 0),
            "w" => new Point(-2, 0),
            "ne" => new Point(1, 1),
            "se" => new Point(1, -1),
            "nw" => new Point(-1, 1),
            "sw" => new Point(-1, -1),
            _ => throw new PuzzleException("Direction not recognised")
        };
    }

    private void ParseInput()
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