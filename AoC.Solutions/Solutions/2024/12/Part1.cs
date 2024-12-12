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
                if (_visited.Add((x, y)))
                {
                    continue;
                }
                
                MapRegion(x, y);
            }
        }
    }

    private void MapRegion(int x, int y)
    {
        var region = new List<(int X, int Y)>();
        
        _regions.Add((_map[x, y], region));

        _visited.Add((x, y));
        
        _queue.Enqueue((x, y));

        while (_queue.Count > 0)
        {
            var cell = _queue.Dequeue();

            if (_visited.Contains(cell))
            {
                continue;
            }

            _visited.Add((x, y));
            
            region.Add((x, y));
            
            SafeEnqueue(x, y);
            SafeEnqueue(x - 1, y);
            SafeEnqueue(x, y - 1);
            SafeEnqueue(x - 1, y - 1);
        }
    }

    private void SafeEnqueue(int x, int y)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height)
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