using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._12;

public abstract class Base : Solution
{
    public override string Description => "Garden groups";

    protected readonly List<(char Plant, List<(int X, int Y)> Cells)> Regions = [];

    private readonly HashSet<(int X, int Y)> _visited = [];

    private readonly Queue<(int X, int Y)> _queue = [];
    
    private char[,] _map;

    private int _width;

    private int _height;
    
    protected void ParseInput()
    {
        _map = Input.To2DArray();

        _width = _map.GetLength(0);

        _height = _map.GetLength(1);
    }

    protected void FindRegions()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (! _visited.Contains((x, y)))
                {
                    MapRegion(x, y);
                }
            }
        }
    }

    protected int GetPerimeter((char Plant, List<(int X, int Y)> Cells) region)
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
    
    private bool IsEdge(char plant, int x, int y)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height)
        {
            return true;
        }

        return _map[x, y] != plant;
    }

    private void MapRegion(int x, int y)
    {
        var region = new List<(int X, int Y)>();

        var plant = _map[x, y];
        
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
        if (x < 0 || x >= _width || y < 0 || y >= _height)
        {
            return;
        }

        if (_map[x, y] != plant || _visited.Contains((x, y)))
        {
            return;
        }

        _queue.Enqueue((x, y));
    }
}