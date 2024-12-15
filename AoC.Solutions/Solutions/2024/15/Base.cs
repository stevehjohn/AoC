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

        var i = 1;
        
        while (_map[x + i * dX, y + i * dY] is 'O' and not '#')
        {
            i++;
        }

        if (_map[x + i * dX, y + i * dY] == '#')
        {
            return;
        }

        for (; i > 0; i--)
        {
            _map[x + i * dX, y + i * dY] = _map[x + (i - 1) * dX, y + (i - 1) * dY];

            _map[x + (i - 1) * dX, y + (i - 1) * dY] = '.';
        }

        _robot.X = x;

        _robot.Y = y;
    }

    protected long SumCoordinates()
    {
        var sum = 0L;
        
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if (_map[x, y] == 'O')
                {
                    sum += x + y * 100;
                }
            }
        }

        return sum;
    }

    private void Dump()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (x == _robot.X && y == _robot.Y)
                {
                    Console.Write('@');
                    
                    continue;
                }

                Console.Write(_map[x, y]);
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine();
    }

    protected void ParseInput(bool isPart2 = false)
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

        if (isPart2)
        {
            _robot.X *= 2;

            _map = new char[_width * 2, _height];

            for (y = 0; y < _height; y++)
            {
                var line = Input[y];

                for (var x = 0; x < _width; x++)
                {
                    switch (line[x])
                    {
                        case '@':
                        case '.':
                            _map[x * 2, y] = '.';
                            _map[x * 2 + 1, y] = '.';
                            break;

                        case '#':
                            _map[x * 2, y] = '#';
                            _map[x * 2 + 1, y] = '#';
                            break;
                        
                        case 'O':
                            _map[x * 2, y] = '[';
                            _map[x * 2 + 1, y] = ']';
                            break;
                    }
                }
            }
            
            _width *= 2;
            
            Dump();
        }
        else
        {
            _map = Input[..y].To2DArray();

            _map[_robot.X, _robot.Y] = '.';
        }
        
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