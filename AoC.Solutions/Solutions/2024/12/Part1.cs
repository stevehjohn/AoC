using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._12;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<(int X, int Y)> _visited = [];

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
    }

    private void ParseInput()
    {
        _map = Input.To2DArray();

        _width = _map.GetLength(0);

        _height = _map.GetLength(1);
    }
}