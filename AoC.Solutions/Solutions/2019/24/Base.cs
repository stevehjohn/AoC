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

        var bit = 16_777_216;

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

    private static int AdjacentCount(int grid, int bit, int parent, int child)
    {
        var count = 0;

        if ((bit & 17_318_416) == 0 && bit != 2_048)
        {
            count += (grid & (bit << 1)) > 0 ? 1 : 0;
        }

        if ((bit & 1_082_401) == 0 && bit != 8_192)
        {
            count += (grid & (bit >> 1)) > 0 ? 1 : 0;
        }

        if (bit != 128)
        {
            count += (grid & (bit << 5)) > 0 ? 1 : 0;
        }

        if (bit != 131_072)
        {
            count += (grid & (bit >> 5)) > 0 ? 1 : 0;
        }

        if (child > 0)
        {
            if (bit == 128)
            {
                count += CountBits(child & 31);
            }

            if (bit == 2_048)
            {
                count += CountBits(child & 1_082_401);
            }

            if (bit == 8_192)
            {
                count += CountBits(child & 17_318_416);
            }

            if (bit == 131_072)
            {
                count += CountBits(child & 32_505_856);
            }
        }

        return count;
    }

    protected static int CountBits(int value)
    {
        var count = 0;

        while (value > 0)
        {
            count++;

            value &= value - 1;
        }

        return count;
    }
}