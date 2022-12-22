using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._22;

public abstract class Base : Solution
{
    public override string Description => "Monkey map";

    private char[,] _map;

    private int _width;

    private int _height;

    private string _path;

    private Point _position;

    private char _direction = 'R';

    private int _steps;

    protected void ParseInput()
    {
        int y;

        for (y = 0; y < Input.Length; y++)
        {
            var line = Input[y];

            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }

            if (line.Length > _width)
            {
                _width = line.Length;
            }
        }

        _height = y;

        _map = new char[_width, _height];

        for (y = 0; y < _height; y++)
        {
            var line = Input[y];

            for (var x = 0; x < line.Length; x++)
            {
                if (_position == null && line[x] == '.')
                {
                    _position = new Point(x, y);
                }

                if (line[x] != ' ')
                {
                    _map[x, y] = line[x];
                }
            }
        }

        _path = Input[y + 1];

        for (var x = 0; x < _path.Length; x++)
        {
            if (_path[x] > '9')
            {
                _steps++;
            }
        }

        _steps++;
    }

    protected void WalkPath()
    {
        Console.CursorVisible = false;

        var previous = 0;

        for (var i = 0; i < _path.Length; i++)
        {
            if (_path[i] > '9')
            {
                var length = i == _path.Length - 1 ? int.Parse(_path.Substring(i)) : int.Parse(_path.Substring(previous, i - previous));

                Walk(length);

                _direction = (_direction, _path[i]) switch
                {
                    ('R', 'R') => 'D',
                    ('D', 'R') => 'L',
                    ('L', 'R') => 'U',
                    ('U', 'R') => 'R',
                    ('R', 'L') => 'U',
                    ('D', 'L') => 'R',
                    ('L', 'L') => 'D',
                    ('U', 'L') => 'L',
                    _ => throw new PuzzleException("Don't know how to turn.")
                };

                _steps--;

                SmallDump();

                previous = i + 1;
            }
        }
    }

    protected int GetSolution()
    {
        var facing = _direction switch
        {
            'R' => 0,
            'D' => 1,
            'L' => 2,
            'U' => 3,
            _ => throw new PuzzleException("Don't know where I'm facing.")
        };

        return 1_000 * (_position.Y + 1) + 4 * (_position.X + 1) + facing;
    }

    private void Dump()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (_position.X == x && _position.Y == y)
                {
                    Console.Write("@");

                    continue;
                }

                if (_map[x, y] == '\0')
                {
                    Console.Write(" ");

                    continue;
                }

                Console.Write(_map[x, y]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private void Walk(int length)
    {
        switch (_direction)
        {
            case 'R':
                Walk(1, 0, length);
                break;

            case 'D':
                Walk(0, 1, length);
                break;

            case 'L':
                Walk(-1, 0, length);
                break;

            case 'U':
                Walk(0, -1, length);
                break;
        }
    }

    private void SmallDump(Point position = null)
    {
        Console.SetCursorPosition(1, 1);

        Console.Write($"{_direction}: Steps: {_steps}    ");

        var cY = 0;

        if (position == null)
        {
            position = _position;
        }

        for (var y = position.Y - 10; y < position.Y + 10; y++)
        {
            Console.SetCursorPosition(1, 3 + cY);

            for (var x = position.X - 20; x < position.X + 20; x++)
            {
                if (x == position.X && y == position.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write('@');

                    Console.ForegroundColor = ConsoleColor.Green;

                    continue;
                }

                if (x < 0 || x >= _width || y < 0 || y >= _height)
                {
                    Console.Write(' ');

                    continue;
                }

                if (_map[x, y] == '\0')
                {
                    Console.Write(' ');

                    continue;
                }

                Console.Write(_map[x, y]);
            }

            cY++;
        }
    }

    private void Walk(int xD, int yD, int length)
    {
        while (length > 0)
        {
            var p = new Point(_position);

            p.X += xD;
            p.Y += yD;

            if (p.X < 0)
            {
                p.X = _width - 1;
            }

            if (p.X == _width)
            {
                p.X = 0;
            }

            if (p.Y < 0)
            {
                p.Y = _height - 1;
            }

            if (p.Y == _height)
            {
                p.Y = 0;
            }

            var newPosition = _map[p.X, p.Y];

            if (newPosition == '.')
            {
                _position = p;

                length--;

                continue;
            }

            if (newPosition == '#')
            {
                return;
            }

            var position = new Point(_position);

            while (true)
            {
                position.X += xD;
                position.Y += yD;

                if (position.X < 0)
                {
                    position.X = _width - 1;
                }

                if (position.X == _width)
                {
                    position.X = 0;
                }

                if (position.Y < 0)
                {
                    position.Y = _height - 1;
                }

                if (position.Y == _height)
                {
                    position.Y = 0;
                }

                if (_map[position.X, position.Y] == '#')
                {
                    return;
                }

                if (_map[position.X, position.Y] == '.')
                {
                    _position = position;

                    length--;

                    break;
                }
            }
        }
    }
}