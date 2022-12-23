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

    private readonly Dictionary<(Point Segment, char InDirection), (Point NewSegment, char NewDirection, char Edge)> _edgeMappings = new();

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

        if (IsCube)
        {
            InitialiseEdgeMappings();
        }
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

                Dump();

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

    private void InitialiseEdgeMappings()
    {
        // Template
        // _edgeMappings.Add((new Point(), ' '), (new Point(), ' '));

        // Format <Segment you're leaving, Segment you're entering>

        // Test data.
        _edgeMappings.Add((new Point(1, 1), 'U'), (new Point(2, 0), 'R', 'W'));
        _edgeMappings.Add((new Point(2, 1), 'R'), (new Point(3, 2), 'D', 'N'));
        _edgeMappings.Add((new Point(2, 2), 'D'), (new Point(0, 1), 'U', 'S'));

        // Actual data.
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

        var newSegmentInfo = _edgeMappings[(segment, _direction)];

        var segmentStart = new Point(newSegmentInfo.NewSegment.X * FaceSize, newSegmentInfo.NewSegment.Y * FaceSize);

        // Incoming/outgoing edges matter!
        var position = newSegmentInfo.Edge switch
        {
            'N' => new Point(segmentStart.X + (FaceSize - 1 - segmentPosition.Y), segmentStart.Y), // Tweak X
            'E' => new Point(segmentStart.X + FaceSize - 1, segmentStart.Y), // Tweak Y
            'S' => new Point(segmentStart.X + (FaceSize - 1 - segmentPosition.X), segmentStart.Y + FaceSize - 1), // Tweak X
            'W' => new Point(segmentStart.X, segmentStart.Y + segmentPosition.X), // Tweak Y
            _ => throw new PuzzleException("Unknown segment edge.")
        };

        _direction = newSegmentInfo.NewDirection;

        if (_map[position.X, position.Y] == '#')
        {
            _direction = previousDirection;

            return 0;
        }

        _position = position;

        return length - 1;
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

    private void Dump()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (_position.X == x && _position.Y == y)
                {
                    switch (_direction)
                    {
                        case 'U':
                            Console.Write("^");
                            break;
                        case 'R':
                            Console.Write(">");
                            break;
                        case 'D':
                            Console.Write("v");
                            break;
                        case 'L':
                            Console.Write("<");
                            break;
                    }

                    continue;
                }

                if (_map[x, y] == '\0')
                {
                    Console.Write(' ');

                    continue;
                }

                Console.Write(_map[x, y]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}