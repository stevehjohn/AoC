using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._11;

[UsedImplicitly]
public class Part1 : Solution
{
    private int _width;

    private int _height;

    private int[,] _grid;

    public override string GetAnswer()
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

        for (var i = 0; i < 100; i++)
        {
            IncrementGrid();

            flashes += DoFlashes();
            
            ResetFlashers();
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

        var flashes = 0;

        while (flashed)
        {
            flashed = false;

            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (_grid[x, y] == 10)
                    {
                        _grid[x, y]++;

                        flashes++;

                        flashed = true;

                        Flash(x, y);

                        break;
                    }

                    if (flashed)
                    {
                        break;
                    }
                }
            }
        }

        return flashes;
    }

    private void Flash(int x, int y)
    {
        if (y > 0)
        {
            if (x > 0)
            {
                _grid[x - 1, y - 1]++;
            }

            _grid[x, y - 1]++;

            if (x < _width - 1)
            {
                _grid[x + 1, y - 1]++;
            }
        }

        if (x > 0)
        {
            _grid[x - 1, y]++;
        }

        if (x < _width - 1)
        {
            _grid[x + 1, y]++;
        }

        if (y < _height - 1)
        {
            if (x > 0)
            {
                _grid[x - 1, y + 1]++;
            }

            _grid[x, y + 1]++;

            if (x < _width - 1)
            {
                _grid[x + 1, y + 1]++;
            }
        }
    }

    private void ResetFlashers()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (_grid[x, y] > 9)
                {
                    _grid[x, y] = 0;
                }
            }
        }
    }
}