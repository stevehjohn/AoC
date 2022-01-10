using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._24;

public abstract class Base : Solution
{
    public override string Description => "Wastl's game of life";

    protected readonly bool[,] Grid = new bool[7, 7];

    protected void PlayRound()
    {
        var dies = new List<Point>();

        var infests = new List<Point>();

        for (var y = 1; y < 6; y++)
        {
            for (var x = 1; x < 6; x++)
            {
                var adjacent = AdjacentCount(x, y);

                if (Grid[x, y] && adjacent != 1)
                {
                    dies.Add(new Point(x, y));
                }

                if (! Grid[x, y] && (adjacent == 1 || adjacent == 2))
                {
                    infests.Add(new Point(x, y));
                }
            }
        }

        dies.ForEach(d => Grid[d.X, d.Y] = false);

        infests.ForEach(i => Grid[i.X, i.Y] = true);
    }

    private int AdjacentCount(int x, int y)
    {
        var count = Grid[x - 1, y] ? 1 : 0;

        count += Grid[x, y - 1] ? 1 : 0;

        count += Grid[x + 1, y] ? 1 : 0;

        count += Grid[x, y + 1] ? 1 : 0;

        return count;
    }
}