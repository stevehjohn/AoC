using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._22;

public abstract class Base : Solution
{
    public override string Description => "Monkey map";

    // 4 for sample input, 50 for actual.
    private const int FaceSize = 4;

    protected bool IsCube { get; set; }

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
                _ => throw new PuzzleException("")
            };

            var previousPosition = new Point(_position);

            var position = MoveOneStep(previousPosition, xD, yD, ! IsCube);

            if (position.X < 0 || position.X == _width || position.Y < 0 || position.Y == _height)
            {
                length = Teleport3D(previousPosition, length);

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

            if (IsCube)
            {
                length = Teleport3D(previousPosition, length);
            }
            else
            {
                length = TeleportFlat(position, xD, yD, length);
            }
        }
    }

    private int TeleportFlat(Point point, int xD, int yD, int length)
    {
        while (true)
        {
            point = MoveOneStep(point, xD, yD, true);

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

    // TODO: *Shrugs*
    private int Teleport3D(Point point, int length)
    {
        var segment = new Point(point.X / FaceSize, point.Y / FaceSize);

        var segmentPosition = new Point(point.X % FaceSize, point.Y % FaceSize);

        var previousDirection = _direction;

        // For test data.
        (var position, _direction) = (segment.X, segment.Y, _direction) switch
        {
            (1, 1, 'U') => (GetPositionAfterTeleport(segmentPosition, new Point(2, 0), 'W'), 'R'),
            (2, 1, 'R') => (GetPositionAfterTeleport(segmentPosition, new Point(3, 2), 'N'), 'D'),
            (2, 2, 'D') => (GetPositionAfterTeleport(segmentPosition, new Point(0, 1), 'S'), 'U'),
            //(2, 2, 'D') => (new Point(FaceSize - 1 - segmentPosition.X, FaceSize * 2 - 1), 'U'),
            _ => throw new PuzzleException("Unknown map segment.")
        };

        //(var position, _direction) = (segmentIndex, _direction) switch
        //{
        //    (0, 'L') => (new Point(), ' '),
        //    _ => throw new PuzzleException("Unknown map segment.")
        //};

        if (_map[position.X, position.Y] == '#')
        {
            _direction = previousDirection;

            return 0;
        }

        _position = position;

        return length - 1;
    }

    private static Point GetPositionAfterTeleport(Point segmentPosition, Point newSegment, char edge)
    {
        var segment = new Point(newSegment.X * FaceSize, newSegment.Y * FaceSize);

        switch (edge)
        {
            case 'N':
                segment.X += FaceSize - 1 - segmentPosition.Y;
                return segment;

            case 'W':
                segment.Y += segmentPosition.X;
                return segment;

            case 'S':
                segment.Y += FaceSize - 1;
                segment.X += FaceSize - 1 - segmentPosition.X;
                return segment;

            case 'E':
                break;
        }

        return null;
    }

    private Point MoveOneStep(Point point, int xD, int yD, bool wrap)
    {
        var newPoint = new Point(point);

        newPoint.X += xD;
        newPoint.Y += yD;

        if (! wrap && (newPoint.X < 0 || newPoint.X == _width || newPoint.Y < 0 || newPoint.Y == _height))
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