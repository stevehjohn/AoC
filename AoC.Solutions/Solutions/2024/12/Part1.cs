using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        FindRegions();

        var cost = 0;

        for (var i = 0; i < Regions.Count; i++)
        {
            var region = Regions[i];

            cost += region.Cells.Count * GetPerimeter(region);
        }

        return cost.ToString();
    }

    private int GetPerimeter((char Plant, List<(int X, int Y)> Cells) region)
    {
        var perimeter = 0;
        
        for (var i = 0; i < region.Cells.Count; i++)
        {
            var cell = region.Cells[i];

            perimeter += IsEdge(region.Plant, cell.X - 1, cell.Y) ? 1 : 0;
            perimeter += IsEdge(region.Plant, cell.X + 1, cell.Y) ? 1 : 0;
            perimeter += IsEdge(region.Plant, cell.X, cell.Y - 1) ? 1 : 0;
            perimeter += IsEdge(region.Plant, cell.X, cell.Y + 1) ? 1 : 0;
        }

        return perimeter;
    }
}