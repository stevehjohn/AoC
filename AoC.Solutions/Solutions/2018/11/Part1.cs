using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        CalculateCellPowers(ParseInput());

        var result = GetMaxPower();

        return $"{result.X},{result.Y}";
    }

    private Point GetMaxPower()
    {
        var max = 0;

        Point position = null;

        for (var y = 1; y < GridSize - 1; y++)
        {
            for (var x = 1; x < GridSize - 1; x++)
            {
                var sum = Grid[x - 1, y - 1] + Grid[x, y - 1] + Grid[x + 1, y - 1] + Grid[x - 1, y] + Grid[x, y] + Grid[x + 1, y] + Grid[x - 1, y + 1] + Grid[x, y + 1] + Grid[x + 1, y + 1];

                if (sum > max)
                {
                    max = sum;

                    position = new Point(x - 1, y - 1);
                }
            }
        }

        if (position == null)
        {
            throw new PuzzleException("Solution not found.");
        }

        return position;
    }
}