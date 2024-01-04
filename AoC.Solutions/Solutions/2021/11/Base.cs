using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._11;

public abstract class Base : Solution
{
    public override string Description => "Flashers";

    private int _width;

    private int _height;

    private int[,] _grid;

    protected string GetAnswer(int iterations, bool endWhenAllFlash = false)
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _grid = new int[_width, _height];

        for (var y = 0; y < Input.Length; y++)
        {
            for (var x = 0; x < Input[y].Length; x++)
            {
                _grid[x, y] = Input[y][x] - '0';
            }
        }

        var flashes = 0;

        for (var i = 0; i < iterations; i++)
        {
            IncrementGrid();

            var flashCount = DoFlashes();

            if (flashCount == _width * _height && endWhenAllFlash)
            {
                return (i + 1).ToString();
            }

            flashes += flashCount;
        }

        return flashes.ToString();
    }

    private void IncrementGrid()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _grid[x, y]++;
            }
        }
    }

    private int DoFlashes()
    {
        var flashed = true;

        var pointsFlashed = new List<Point>();

        while (flashed)
        {
            flashed = false;

            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (_grid[x, y] != 10)
                    {
                        continue;
                    }

                    if (pointsFlashed.Any(p => p.X == x && p.Y == y))
                    {
                        continue;
                    }

                    pointsFlashed.Add(new Point(x, y));

                    var neighbors = GetNeighbors(new Point(x, y));

                    foreach (var neighbor in neighbors)
                    {
                        if (_grid[neighbor.X, neighbor.Y] < 10)
                        {
                            _grid[neighbor.X, neighbor.Y]++;

                            flashed = true;
                        }
                    }
                }
            }
        }

        var flashers = 0;

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (_grid[x, y] == 10)
                {
                    flashers++;

                    _grid[x, y] = 0;
                }
            }
        }

        return flashers;
    }

    private List<Point> GetNeighbors(Point point)
    {
        var neighbors = new List<Point>();

        for (var x = Math.Max(point.X - 1, 0); x <= Math.Min(point.X + 1, _width - 1); x++)
        {
            for (var y = Math.Max(point.Y - 1, 0); y <= Math.Min(point.Y + 1, _height - 1); y++)
            {
                if (x == point.X && y == point.Y)
                {
                    continue;
                }

                neighbors.Add(new Point(x, y));
            }
        }

        return neighbors;
    }
}