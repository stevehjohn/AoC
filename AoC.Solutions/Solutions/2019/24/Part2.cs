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
            grids = PlayLevels(grids);
        }

        var count = 0;

        foreach (var grid in grids)
        {
            Dump(grid);

            count += CountBits(grid);
        }

        return count.ToString();
    }

    private static List<int> PlayLevels(List<int> grids)
    {
        var newGrids = new List<int>();

        var child = PlayRound(0, grids.First());

        if (child > 0)
        {
            newGrids.Add(child);
        }

        for (var i = 0; i < grids.Count; i++)
        {
            var result = PlayRound(grids[i], i < grids.Count - 1 ? grids[i + 1] : 0, child);

            //if (result != 0)
            {
                newGrids.Add(result);
            }

            child = result;
        }

        var parent = PlayRound(0, 0, grids.Last());

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

    private static void Dump(int grid)
    {
        var bit = 1;

        for (var y = 0; y < 5; y++)
        {
            for (var x = 0; x < 5; x++)
            {
                Console.Write((grid & bit) > 0 ? '#' : ' ');

                bit <<= 1;
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}