using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var grids = new List<int>
                    {
                        ParseInput()
                    };

        for (var i = 0; i < 10; i++)
        {
            grids = PlayRound(grids);
        }

        foreach (var grid in grids)
        {
            Dump(grid);
        }

        return "TESTING";
    }

    private List<int> PlayRound(List<int> grids)
    {
        var newGrids = new List<int>();

        var parent = GetParent(grids.Last());

        if (parent > 0)
        {
            newGrids.Add(parent);
        }

        return newGrids;
    }

    private int GetParent(int grid)
    {
        var parent = 0;

        if (CountBits(grid & 31) is 1 or 2)
        {
            parent += 128;
        }

        if (CountBits(grid & 1_082_401) is 1 or 2)
        {
            parent += 2_048;
        }

        if (CountBits(grid & 17_318_416) is 1 or 2)
        {
            parent += 8_192;
        }

        if (CountBits(grid & 32505856) is 1 or 2)
        {
            parent += 131_072;
        }

        return parent;
    }

    private static int CountBits(int value)
    {
        var count = 0;

        while (value > 0)
        {
            count++;

            value &= value - 1;
        }

        return count;
    }

    private void Dump(int grid)
    {
        var bit = 16_777_216;

        for (var y = 0; y < 5; y++)
        {
            for (var x = 0; x < 5; x++)
            {
                Console.Write((grid & bit) > 0 ? '#' : ' ');

                bit >>= 1;
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}