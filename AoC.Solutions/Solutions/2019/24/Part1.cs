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
                                 grid
                             };

        while (true)
        {
            grid = PlayRound(grid);

            if (previousStates.Contains(grid))
            {
                return grid.ToString();
            }

            previousStates.Add(grid);
        }
    }

    private static int PlayRound(int grid)
    {
        var dies = 33_554_431;

        var infests = 0;

        var bit = 16_777_216;

        for (var i = 0; i < 25; i++)
        {
            var adjacent = AdjacentCount(grid, bit);

            if ((grid & bit) > 0 && adjacent != 1)
            {
                dies ^= bit;
            }

            if ((grid & bit) == 0 && (adjacent == 1 || adjacent == 2))
            {
                infests |= bit;
            }

            bit >>= 1;
        }

        grid &= dies;

        grid |= infests;

        return grid;
    }

    private static int AdjacentCount(int grid, int bit)
    {
        var count = 0;

        if ((bit & 17_318_416) == 0)
        {
            count += (grid & (bit << 1)) > 0 ? 1 : 0;
        }

        if ((bit & 1_082_401) == 0)
        {
            count += (grid & (bit >> 1)) > 0 ? 1 : 0;
        }

        count += (grid & (bit << 5)) > 0 ? 1 : 0;

        count += (grid & (bit >> 5)) > 0 ? 1 : 0;

        return count;
    }
}