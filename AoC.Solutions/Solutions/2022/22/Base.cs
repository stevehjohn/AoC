#define NOT_TEST
using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._22;

public abstract class Base : Solution
{
    public override string Description => "Monkey map";

    protected bool IsCube { get; set; }

#if TEST
    private const int FaceSize = 4;
#else
    private const int FaceSize = 50;
#endif

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
            if (_path[i] > '9' || i == _path.Length - 1)
            {
                var length = i == _path.Length - 1 ? int.Parse(_path.Substring(previous)) : int.Parse(_path.Substring(previous, i - previous));

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

    private void Walk(int length)
    {
        while (length > 0)
        {
            var (xD, yD) = _direction switch
            {
                'R' => (1, 0),
                'D' => (0, 1),
                'L' => (-1, 0),
                'U' => (0, -1),
                _ => throw new PuzzleException("Don't know where to turn.")
            };

            var previousPosition = new Point(_position);

            var position = MoveOneStep(previousPosition, xD, yD);

            if (IsCube && (position.X < 0 || position.X == _width || position.Y < 0 || position.Y == _height || _map[position.X, position.Y] == '\0'))
            {
                length = Teleport3D(position, length);

                continue;
            }

            var tile = _map[position.X, position.Y];

            if (tile == '.')
            {
                _position = position;

                length--;

                continue;
            }

            if (tile == '#')
            {
                return;
            }

            length = TeleportFlat(position, xD, yD, length);
        }
    }

    private int TeleportFlat(Point point, int xD, int yD, int length)
    {
        while (true)
        {
            point = MoveOneStep(point, xD, yD);

            if (_map[point.X, point.Y] == '#')
            {
                return 0;
            }

            if (_map[point.X, point.Y] == '.')
            {
                _position = point;

                length--;

                break;
            }
        }

        return length;
    }

    private int Teleport3D(Point position, int length)
    {
        var segmentPosition = new Point(position.X % FaceSize, position.Y % FaceSize);

        var newSegment = new Point(position.X < 0 ? -1 : position.X / FaceSize, position.Y < 0 ? -1 : position.Y / FaceSize);

        (position, var newDirection) = Wrap3D(newSegment, segmentPosition);

        if (_map[position.X, position.Y] == '#')
        {
            return 0;
        }

        _position = position;

        _direction = newDirection;

        return length - 1;
    }

    // TODO: This is the bit I'd like to programatically calculate. It hurts my brain.
#if TEST
    private (Point Position, char NewDirection) Wrap3D(Point newSegment, Point segmentPosition)
    {
        return (newSegment.X, newSegment.Y, _direction) switch
        {
            (3, 1, 'R') => (GetPositionInNewSegment(3, 2, 'U', segmentPosition.Y), 'D'),
            (2, 3, 'D') => (GetPositionInNewSegment(0, 1, 'D', segmentPosition.X), 'U'),
            (1, 0, 'U') => (GetPositionInNewSegment(2, 0, 'L', segmentPosition.X, false), 'R'),
            _ => throw new PuzzleException("Cannot 3D teleport.")
        };
    }
#else
    private (Point Position, char NewDirection) Wrap3D(Point newSegment, Point segmentPosition)
    {
        return (newSegment.X, newSegment.Y, _direction) switch
        {
            (0, 0, 'L') => (GetPositionInNewSegment(0, 2, 'L', segmentPosition.Y), 'R'), // ✓
            (1, -1, 'U') => (GetPositionInNewSegment(0, 3, 'L', segmentPosition.X, false), 'R'), // ✓
            (2, -1, 'U') => (GetPositionInNewSegment(0, 3, 'D', segmentPosition.X, false), 'U'), // ✓
            (3, 0, 'R') => (GetPositionInNewSegment(1, 2, 'R', segmentPosition.Y), 'L'), // ✓
            (2, 1, 'D') => (GetPositionInNewSegment(1, 1, 'R', segmentPosition.X, false), 'L'), // ✓
            (2, 1, 'R') => (GetPositionInNewSegment(2, 0, 'D', segmentPosition.Y, false), 'U'), // ✓
            (2, 2, 'R') => (GetPositionInNewSegment(2, 0, 'R', segmentPosition.Y), 'L'), // ✓
            (1, 3, 'D') => (GetPositionInNewSegment(0, 3, 'R', segmentPosition.X, false), 'L'), // ✓
            (1, 3, 'R') => (GetPositionInNewSegment(1, 2, 'D', segmentPosition.Y, false), 'U'), // ✓
            (0, 4, 'D') => (GetPositionInNewSegment(2, 0, 'U', segmentPosition.X, false), 'D'), // ✓
            (-1, 3, 'L') => (GetPositionInNewSegment(1, 0, 'U', segmentPosition.Y, false), 'D'), // ✓
            (-1, 2, 'L') => (GetPositionInNewSegment(1, 0, 'L', segmentPosition.Y), 'R'), // ✓
            (0, 1, 'U') => (GetPositionInNewSegment(1, 1, 'L', segmentPosition.X, false), 'R'), // ✓
            (0, 1, 'L') => (GetPositionInNewSegment(0, 2, 'U', segmentPosition.Y, false), 'D'), // ✓
            _ => throw new PuzzleException("Cannot 3D teleport.")
        };
    }
#endif

    private static Point GetPositionInNewSegment(int x, int y, char edge, int delta, bool reverse = true)
    {
        var position = new Point(x * FaceSize, y * FaceSize);

        switch (edge)
        {
            case 'U':
                position.X += reverse ? FaceSize - 1 - delta : delta;
                break;

            case 'R':
                position.X += FaceSize - 1;
                position.Y += reverse ? FaceSize - 1 - delta : delta;
                break;

            case 'D':
                position.X += reverse ? FaceSize - 1 - delta : delta;
                position.Y += FaceSize - 1;
                break;

            case 'L':
                position.Y += reverse ? FaceSize - 1 - delta : delta;
                break;
        }

        return position;
    }

    private Point MoveOneStep(Point point, int xD, int yD)
    {
        var newPoint = new Point(point);

        newPoint.X += xD;
        newPoint.Y += yD;

        if (IsCube)
        {
            return newPoint;
        }

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