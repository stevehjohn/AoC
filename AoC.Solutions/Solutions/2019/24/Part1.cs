using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._24;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var grid = ParseInput();

        var previousStates = new HashSet<int>
                             {
                                 GetBiodiversity(grid)
                             };

        while (true)
        {
            PlayRound(grid);

            var bioDiversity = GetBiodiversity(grid);

            if (previousStates.Contains(bioDiversity))
            {
                return bioDiversity.ToString();
            }

            previousStates.Add(bioDiversity);
        }
    }

    protected static void PlayRound(bool[,] grid)
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

    private static int GetBiodiversity(bool[,] grid)
    {
        var i = 1;

        var diversity = 0;

        for (var y = 1; y < 6; y++)
        {
            for (var x = 1; x < 6; x++)
            {
                if (grid[x, y])
                {
                    diversity += i;
                }

                i *= 2;
            }
        }

        return diversity;
    }
}