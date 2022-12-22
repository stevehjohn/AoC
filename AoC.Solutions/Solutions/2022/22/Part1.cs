using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._22;

public class Part1 : Base
{
    private char[,] _map;

    private int _width;

    private int _height;

    private string _path;

    private Point _position;

    private char _direction = 'R';

    public override string GetAnswer()
    {
        ParseInput();

        WalkPath();

        return GetSolution().ToString();
    }

    private void ParseInput()
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

    private void WalkPath()
    {
        Console.CursorVisible = false;

        var previous = 0;

        for (var i = 0; i < _path.Length; i++)
        {
            if (_path[i] > '9' || i == _path.Length - 1)
            {
                var length = i == _path.Length - 1 ? int.Parse(_path.Substring(i)) : int.Parse(_path.Substring(previous, i - previous));

                Walk(length);

                if (i == _path.Length - 1)
                {
                    break;
                }

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

    private int GetSolution()
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
            var newPoint = NewPosition(_position, xD, yD);

            var newPosition = _map[newPoint.X, newPoint.Y];

            if (newPosition == '.')
            {
                _position = newPoint;

                length--;

                continue;
            }

            if (newPosition == '#')
            {
                return;
            }

            while (true)
            {
                newPoint = NewPosition(newPoint, xD, yD);

                if (_map[newPoint.X, newPoint.Y] == '#')
                {
                    return;
                }

                if (_map[newPoint.X, newPoint.Y] == '.')
                {
                    _position = newPoint;

                    length--;

                    break;
                }
            }
        }
    }

    private Point NewPosition(Point point, int xD, int yD)
    {
        var newPoint = new Point(point);

        newPoint.X += xD;
        newPoint.Y += yD;

        if (newPoint.X < 0)
        {
            newPoint.X = _width - 1;
        }

        if (newPoint.X == _width)
        {
            newPoint.X = 0;
        }

        if (newPoint.Y < 0)
        {
            newPoint.Y = _height - 1;
        }

        if (newPoint.Y == _height)
        {
            newPoint.Y = 0;
        }

        return newPoint;
    }
}