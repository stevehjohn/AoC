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

        count += WalkEdge(region.Plant, region.Cells[0].X, region.Cells[0].Y);
        
        return count;
    }

    private int WalkEdge(char plant, int startX, int startY)
    {
        var turns = 0;
        
        var x = startX;

        var y = startY;

        var dX = 1;

        var dY = 0;
        
        do
        {
            x += dX;

            y += dY;

            if (x < 0 || x >= Width || y < 0 || y >= Height || Map[x, y] != plant)
            {
                x -= dX;

                y -= dY;

                dX = -dY;

                dY = dX;

                turns++;
            }

        } while (x != startX && y != startY);

        return turns;
    }
}