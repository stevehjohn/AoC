using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._24;

public abstract class Base : Solution
{
    public override string Description => "Wastl's game of life";

    protected void PlayRound(bool[,] grid)
    {
        var dies = new List<Point>();

        var infests = new List<Point>();

        for (var y = 1; y < 6; y++)
        {
            for (var x = 1; x < 6; x++)
            {
                var adjacent = AdjacentCount(grid, x, y);

                if (grid[x, y] && adjacent != 1)
                {
                    dies.Add(new Point(x, y));
                }

                if (! grid[x, y] && (adjacent == 1 || adjacent == 2))
                {
                    infests.Add(new Point(x, y));
                }
            }
        }

        dies.ForEach(d => grid[d.X, d.Y] = false);

        infests.ForEach(i => grid[i.X, i.Y] = true);
    }

    private static int AdjacentCount(bool[,] grid, int x, int y)
    {
        var count = grid[x - 1, y] ? 1 : 0;

        count += grid[x, y - 1] ? 1 : 0;

        count += grid[x + 1, y] ? 1 : 0;

        count += grid[x, y + 1] ? 1 : 0;

        return count;
    }

    protected bool[,] ParseInput()
    {
        var result = new bool[7, 7];

        var y = 1;

        foreach (var line in Input)
        {
            for (var x = 1; x < 6; x++)
            {
                result[x, y] = line[x - 1] == '#';
            }

            y++;
        }

        return result;
    }
}