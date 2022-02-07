using AoC.Solutions.Extensions;
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

        for (var i = 0; i < 200; i++)
        {
            grids = PlayLevels(grids);
        }

        var count = 0;

        foreach (var grid in grids)
        {
            count += grid.CountBits();
        }

        return count.ToString();
    }

    private static List<int> PlayLevels(List<int> grids)
    {
        var newGrids = new List<int>();

        for (var i = 0; i < grids.Count; i++)
        {
            var result = PlayRound(grids[i], i < grids.Count - 1 ? grids[i + 1] : 0, i > 0 ? grids[i - 1] : 0);

            newGrids.Add(result);
        }

        var child = PlayRound(0, grids.First());

        if (child > 0)
        {
            newGrids.Insert(0, child);
        }

        var parent = PlayRound(0, 0, grids.Last());

        if (parent > 0)
        {
            newGrids.Add(parent);
        }

        return newGrids;
    }
}