using System.Drawing;
using System.Text;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._15;

public abstract class Base : Solution
{
    public override string Description => "Warehouse woes";

    private char[,] _map;

    private int _width;

    private int _height;

    private string _directions;

    private Point _robot;
    
    protected void ParseInput()
    {
        _width = Input[0].Length;
        
        int y;
        
        for (y = 0; y < Input.Length; y++)
        {
            var line = Input[y];

            if (_robot == default)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (line[x] == '@')
                    {
                        _robot = new Point(x, y);
                        
                        break;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(Input[y]))
            {
                break;
            }
        }

        _height = y;

        _map = Input[..y].To2DArray();

        y++;

        var builder = new StringBuilder();
        
        while (y < Input.Length)
        {
            builder.Append(Input[y]);

            y++;
        }

        _directions = builder.ToString();
    }
}