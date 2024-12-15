using System.Drawing;
using System.Text;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._15;

public abstract class Base : Solution
{
    public override string Description => "Warehouse woes";

    protected bool IsPart2;
    
    private char[,] _map;

    private char[] _pushMask;

    private int _width;

    private int _height;

    private string _directions;

    private Point _robot;

    protected void RunRobot()
    {
        Console.Clear();
        
        for (var i = 0; i < _directions.Length; i++)
        {
            Console.CursorLeft = 0;

            Console.CursorTop = 0;
            
            DumpMap();
            
            Console.WriteLine();
            
            Console.WriteLine($"{i}: {_directions[i]}");

            if (i > 500)
            {
                Console.ReadKey();
            }

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

        if (dY != 0 && IsPart2)
        {
            TryPushVertically(x, y, dY);
            
            return;
        }

        var i = 1;
        
        while (_map[x + i * dX, y + i * dY] is ('O' or '[' or ']') and not '#')
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

    private void TryPushVertically(int x, int y, int dY)
    {
        Array.Fill(_pushMask, '.');

        _pushMask[x] = 'O';

        if (_map[x, y] == '[')
        {
            _pushMask[x + 1] = 'O';
        }
        else
        {
            _pushMask[x - 1] = 'O';
        }

        var cY = y;
        
        var clear = true;

        while (cY > 0 && cY < _height - 1)
        {
            cY += dY;

            var pushed = false;
            
            for (var i = 2; i < _width - 2; i++)
            {
                if (_pushMask[i] == 'O' && _map[i, cY] == '#')
                {
                    clear = false;
                    
                    break;
                }

                if (_map[i, cY] is '.' or '#')
                {
                    continue;
                }

                if (_pushMask[i] != 'O')
                {
                    continue;
                }

                _pushMask[i] = 'O';
                
                if (_map[i, cY] == '[')
                {
                    _pushMask[i + 1] = 'O';
                }
                else
                {
                    _pushMask[i - 1] = 'O';
                }

                pushed = true;
            }

            if (clear && ! pushed)
            {
                break;
            }
        }

        if (clear)
        {
            Console.WriteLine();
            
            for (var i = 0; i < _width; i++)
            {
                Console.Write(_pushMask[i]);
            }

            Console.WriteLine($" {cY}");
            
            PushVertically(y, dY, cY);

            _robot.X = x;

            _robot.Y = y;
        }
    }

    private void PushVertically(int y, int dY, int cY)
    {
        while (cY != y)
        {
            for (var i = 2; i < _width - 2; i++)
            {
                if (_pushMask[i] == 'O' && _map[i, cY] == '.')
                {
                    _map[i, cY] = _map[i, cY - dY];

                    _map[i, cY - dY] = '.';
                }
            }

            // Console.CursorLeft = 0;
            //
            // Console.CursorTop = 0;

            cY -= dY;

            for (var i = 2; i < _width - 2; i++)
            {
                if (_pushMask[i] != '.')
                {
                    if (_map[i, cY - dY] is ']' or '#')
                    {
                        _pushMask[i] = '.';
                    }

                    break;
                }
            }
            
            for (var i = _width - 2; i > 2; i--)
            {
                if (_pushMask[i] != '.')
                {
                    if (_map[i, cY - dY] is '[' or '#')
                    {
                        _pushMask[i] = '.';
                    }

                    break;
                }
            }
            
            // DumpMap();
            //
            // Console.WriteLine();
            //
            // for (var i = 0; i < _width; i++)
            // {
            //     Console.Write(_pushMask[i]);
            // }
            //
            // Console.WriteLine($" {cY}");
            //
            // Console.ReadKey();
        }
    }

    protected long SumCoordinates()
    {
        var sum = 0L;
        
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if (_map[x, y] is 'O' or '[')
                {
                    sum += x + y * 100;
                }
            }
        }

        return sum;
    }

    private void DumpMap()
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

        if (IsPart2)
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

            _pushMask = new char[_width];
            
            DumpMap();
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