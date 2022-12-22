using System.Net.NetworkInformation;
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
    }

    protected void WalkPath()
    {
        var previous = 0;

        for (var i = 0; i < _path.Length; i++)
        {
            if (_path[i] > '9')
            {
                var length = i == _path.Length - 1 ? int.Parse(_path.Substring(i)) : int.Parse(_path.Substring(previous, i - previous));

                Walk(length);

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

                previous = i + 1;
            }
        }
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

    private void Walk(int xD, int yD, int length)
    {
        while (length > 0)
        {
            var newPosition = _map[_position.X + xD, _position.Y + yD];

            if (newPosition == '.')
            {
                _position.X += xD;
                _position.Y += yD;

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