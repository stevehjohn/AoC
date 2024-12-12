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

            cost += region.Cells.Count * CountRegionCorners(region);
        }

        return cost.ToString();
    }
    private int CountRegionCorners((char Plant, List<(int X, int Y)> Cells) region)
    {
        var cornerCount = 0;

        for (var i = 0; i < region.Cells.Count; i++)
        {
            cornerCount += CountCellCorners(region.Plant, region.Cells[i].X, region.Cells[i].Y);
        }

        return cornerCount;
    }

    private int CountCellCorners(char plant, int x, int y)
    {
        var dX = -1;

        var dY = 0;

        var count = 0;
        
        for (var i = 0; i < 4; i++)
        {
            if (GetValueOrNull(x + dX, y + dY) != plant
                && GetValueOrNull(x + dY, y - dX) != plant)
            {
                count++;
            }

            if (GetValueOrNull(x + dX, y + dY) == plant
                && GetValueOrNull(x + dY, y - dX) == plant
                && GetValueOrNull(x + dX + dY, y + dY - dX) != plant)
            {
                count++;
            }

            (dX, dY) = (dY, -dX);
        }

        return count;
    }

    private char GetValueOrNull(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return '\0';
        }

        return Map[x, y];
    }
}