using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        FindRegions();

        var cost = 0;

        for (var i = 0; i < Regions.Count; i++)
        {
            var region = Regions[i];

            cost += region.Cells.Count * CountCorners(region);
        }

        return cost.ToString();
    }
    private int CountCorners((char Plant, List<(int X, int Y)> Cells) region)
    {
        var cornerCount = 0;

        return cornerCount;
    }
}