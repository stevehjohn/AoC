using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._12;

public abstract class Base : Solution
{
    public override string Description => "Garden groups";

    protected readonly List<(char Plant, List<(int X, int Y)> Cells)> Regions = [];

    protected char[,] Map;

    protected int Width;

    protected int Height;
    
    private readonly HashSet<(int X, int Y)> _visited = [];

    private readonly Queue<(int X, int Y)> _queue = [];
    
    protected void ParseInput()
    {
        Map = Input.To2DArray();

        Width = Map.GetLength(0);

        Height = Map.GetLength(1);
    }

    protected void FindRegions()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (! _visited.Contains((x, y)))
                {
                    MapRegion(x, y);
                }
            }
        }
    }

    private void MapRegion(int x, int y)
    {
        var region = new List<(int X, int Y)>();

        var plant = Map[x, y];
        
        Regions.Add((plant, region));
        
        _queue.Enqueue((x, y));

        while (_queue.Count > 0)
        {
            var cell = _queue.Dequeue();

            if (! _visited.Add((cell.X, cell.Y)))
            {
                continue;
            }

            region.Add((cell.X, cell.Y));
            
            SafeEnqueue(plant, cell.X + 1, cell.Y);
            SafeEnqueue(plant, cell.X - 1, cell.Y);
            SafeEnqueue(plant, cell.X, cell.Y + 1);
            SafeEnqueue(plant, cell.X, cell.Y - 1);
        }
    }

    private void SafeEnqueue(char plant, int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return;
        }

        if (Map[x, y] != plant || _visited.Contains((x, y)))
        {
            return;
        }

        _queue.Enqueue((x, y));
    }
}