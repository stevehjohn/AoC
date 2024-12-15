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

    protected void RunRobot()
    {
        for (var i = 0; i < _directions.Length; i++)
        {
            MakeMove(_directions[i]);
        }
    }

    private void MakeMove(char direction)
    {
        var (dX, dY) = direction switch
        {
            '^' => (0, -1),
            'v' => (0, 1),
            '<' => (-1, 0),
            _ => (1, 0)
        };

        var x = _robot.X + dX;

        var y = _robot.Y + dY;

        var cell = _map[x, y];
        
        if (cell == '#')
        {
            return;
        }

        if (cell == '.')
        {
            _robot.X = x;

            _robot.Y = y;
            
            return;
        }
    }

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

        _map[_robot.X, _robot.Y] = '.';

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