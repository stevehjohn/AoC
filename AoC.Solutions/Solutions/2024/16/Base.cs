using System.Drawing;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._16;

public abstract class Base : Solution
{
    public override string Description => "Reindeer maze";

    private char[,] _map;

    private int _width;

    private int _height;

    private Point _start;

    private Point _end;
    
    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            var line = Input[y];
            
            for (var x = 0; x < _width; x++)
            {
                var cell = line[x];

                if (cell == 'S')
                {
                    _start = new Point(x, y);

                    _map[x, y] = '.';
                    
                    continue;
                }

                if (cell == 'E')
                {
                    _end = new Point(x, y);

                    _map[x, y] = '.';
                    
                    continue;
                }

                _map[x, y] = cell;
            }
        }
    }
}