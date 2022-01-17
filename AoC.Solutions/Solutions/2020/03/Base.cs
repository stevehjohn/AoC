using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._03;

public abstract class Base : Solution
{
    public override string Description => "Tobogganing through the trees";

    private bool[,] _map;

    private int _width;

    private int _height;

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new bool[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _map[x, y] = Input[y][x] == '#';
            }
        }
    }

    protected int CountTrees(int right, int down)
    {
        var trees = 0;

        var x = 0;

        for (var y = down; y < _height; y += down)
        {
            x += right;

            if (x >= _width)
            {
                x -= _width;
            }

            trees += _map[x, y] ? 1 : 0;
        }

        return trees;
    }
}