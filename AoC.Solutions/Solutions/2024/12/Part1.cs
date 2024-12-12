using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._12;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<(int X, int Y)> _visited = [];

    private readonly List<(char Plant, List<(int X, int Y)> Cells)> _regions = [];

    private readonly Queue<(int X, int Y)> _queue = [];
    
    private char[,] _map;

    private int _width;

    private int _height;
    
    public override string GetAnswer()
    {
        ParseInput();

        FindRegions();
        
        return "Unknown";
    }

    private void FindRegions()
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

    private void MapRegion(int x, int y)
    {
        var region = new List<(int X, int Y)>();

        var plant = _map[x, y];
        
        _regions.Add((plant, region));
        
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

    private void ParseInput()
    {
        _map = Input.To2DArray();

        _width = _map.GetLength(0);

        _height = _map.GetLength(1);
    }
}