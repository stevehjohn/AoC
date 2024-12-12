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
        var perimeter = GetPerimeterCells(region);

        return 0;
    }

    private HashSet<(int X, int Y)> GetPerimeterCells((char Plant, List<(int X, int Y)> Cells) region)
    {
        var perimeter = new HashSet<(int X, int Y)>();
        
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