using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._06;

public abstract class Base : Solution
{
    public override string Description => "Guard Gallivant";

    private char[,] _map;

    private int _width;

    private int _height;

    private (int X, int Y) _position;

    private readonly HashSet<int> _visited = [];
    
    protected void ParseInput()
    {
        _map = Input.To2DArray();

        _width = _map.GetUpperBound(0);

        _height = _map.GetUpperBound(1);

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if (_map[x, y] == '^')
                {
                    _position.X = x;

                    _position.Y = y;
                    
                    break;
                }
            }
        }
    }

    protected int WalkMap()
    {
        var x = _position.X;

        var y = _position.Y;

        var dX = 0;

        var dY = -1;

        while (true)
        {
            x += dX;

            y += dY;

            if (x < 0 || x > _width || y < 0 || y > _height)
            {
                break;
            }

            if (_map[x, y] == '#')
            {
                x -= dX;

                y -= dY;

                (dX, dY) = (-dY, dX);

                continue;
            }

            _visited.Add(x + y * _width);
        }

        return _visited.Count;
    }
}