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

            cost += region.Cells.Count * CountEdges(region);
        }

        return cost.ToString();
    }

    private int CountEdges((char Plant, List<(int X, int Y)> Cells) region)
    {
        var count = 0;

        var perimeter = GetPerimeterCells(region);
        
        return count;
    }

    private List<(int X, int Y)> GetPerimeterCells((char Plant, List<(int X, int Y)> Cells) region)
    {
        var perimeter = new List<(int, int)>();
        
        for (var i = 0; i < region.Cells.Count; i++)
        {
            var cell = region.Cells[i];

            if (IsEdge(region.Plant, cell.X + 1, cell.Y))
            {
                perimeter.Add((cell.X, cell.Y));
            }
            
            if (IsEdge(region.Plant, cell.X - 1, cell.Y))
            {
                perimeter.Add((cell.X, cell.Y));
            }
            
            if (IsEdge(region.Plant, cell.X, cell.Y + 1))
            {
                perimeter.Add((cell.X, cell.Y));
            }
            
            if (IsEdge(region.Plant, cell.X, cell.Y - 1))
            {
                perimeter.Add((cell.X, cell.Y));
            }
        }

        return perimeter;
    }
}