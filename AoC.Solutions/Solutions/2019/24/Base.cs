using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._24;

public abstract class Base : Solution
{
    public override string Description => "Wastl's game of life";

    protected int ParseInput()
    {
        var result = 0;

        var bit = 1;

        foreach (var line in Input)
        {
            for (var x = 0; x < 5; x++)
            {
                result |= line[x] == '#' ? bit : 0;

                bit <<= 1;
            }
        }

        return result;
    }

    protected static int PlayRound(int grid, int parent = -1, int child = -1)
    {
        var dies = 33_554_431;

        var infests = 0;

        var bit = 1;

        for (var i = 0; i < 25; i++)
        {
            int adjacent;

            if (parent >= 0 || child >= 0)
            {
                adjacent = AdjacentCount(grid, bit, parent, child);
            }
            else
            {
                adjacent = AdjacentCount(grid, bit);
            }

            if ((grid & bit) > 0 && adjacent != 1)
            {
                dies ^= bit;
            }

            if ((grid & bit) == 0 && (adjacent == 1 || adjacent == 2))
            {
                infests |= bit;
            }

            bit <<= 1;
        }

        grid &= dies;

        grid |= infests;

        if (parent >= 0 || child >= 0)
        {
            grid &= 33_550_335;
        }

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

    private static int AdjacentCount(int grid, int bit, int parent, int child)
    {
        var count = 0;

        grid &= 33_550_335;

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

        if (child > 0)
        {
            if (bit == 128)
            {
                count += (child & 31).CountBits();
            }

            if (bit == 2_048)
            {
                count += (child & 1_082_401).CountBits();
            }

            if (bit == 8_192)
            {
                count += (child & 17_318_416).CountBits();
            }

            if (bit == 131_072)
            {
                count += (child & 32_505_856).CountBits();
            }
        }

        if (parent > 0)
        {
            if ((bit & 31) > 0)
            {
                count += (parent & 128) > 0 ? 1 : 0;
            }

            if ((bit & 1_082_401) > 0)
            {
                count += (parent & 2_048) > 0 ? 1 : 0;
            }

            if ((bit & 17_318_416) > 0)
            {
                count += (parent & 8_192) > 0 ? 1 : 0;
            }

            if ((bit & 32_505_856) > 0)
            {
                count += (parent & 131_072) > 0 ? 1 : 0;
            }
        }

        return count;
    }
}