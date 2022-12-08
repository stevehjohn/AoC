using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._08;

public abstract class Base : Solution
{
    public override string Description => "Treetop Tree House";

    private char[,] _matrix;

    private int _size;

    protected int ProcessInput()
    {
        if (Input.Length != Input[0].Length)
        {
            throw new PuzzleException("Input is not square.");
        }

        _size = Input.Length;

        var visible = _size * 4 - 4;

        _matrix = new char[_size, _size];

        for (var y = 0; y < _size; y++)
        {
            for (var x = 0; x < _size; x++)
            {
                _matrix[x, y] = Input[y][x];
            }
        }

        for (var y = 1; y < _size - 1; y++)
        {
            for (var x = 1; x < _size - 1; x++)
            {
                var isVisible = IsVisible(x, y, -1, 0)
                                || IsVisible(x, y, 1, 0)
                                || IsVisible(x, y, 0, -1)
                                || IsVisible(x, y, 0, 1);

                if (isVisible)
                {
                    visible++;
                }
            }
        }

        return visible;
    }

    private bool IsVisible(int x, int y, int dx, int dy)
    {
        var tree = _matrix[x, y];

        do
        {
            x += dx;

            y += dy;

            if (_matrix[x, y] >= tree)
            {
                return false;
            }
        } while (x > 0 && x < _size - 1 && y > 0 && y < _size - 1);

        return true;
    }
}