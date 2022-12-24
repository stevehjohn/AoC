#define TEST
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._22;

public class Part2 : Base
{
#if TEST
    private const int FaceSize = 4;
#else
    private const int FaceSize = 50;
#endif

    private const int FaceEndIndex = FaceSize + 1;

    private readonly (char Tile, Point InitialPosition)[,,] _cube = new (char Tile, Point InitialPosition)[FaceSize + 2, FaceSize + 2, FaceSize + 2];

    private string _path;

    private readonly Dictionary<Point, Func<int, int, Point>> _mappings = new();

    private Point _position;

    private Point _direction = new(1, 0);

    public override string GetAnswer()
    {
        InitialiseMappings();

        ParseInput();

        WalkCube();

        return "";
    }

    // TODO: Would be nice to automatically do this bit.
    private void InitialiseMappings()
    {
#if TEST
        // Top
        _mappings.Add(new Point(2, 0), (x, y) => new Point(x + 1, y + 1, FaceEndIndex));
        // Back
        _mappings.Add(new Point(0, 1), (x, y) => new Point(FaceSize - x, 0, FaceSize - y));
        // Left
        _mappings.Add(new Point(1, 1), (x, y) => new Point(0, y + 1, x + 1));
        // Front
        _mappings.Add(new Point(2, 1), (x, y) => new Point(x + 1, FaceEndIndex, FaceSize - y));
        // Bottom
        _mappings.Add(new Point(2, 2), (x, y) => new Point(x + 1, FaceSize - y));
        // Right
        _mappings.Add(new Point(3, 2), (x, y) => new Point(FaceEndIndex, FaceSize - x, FaceSize - y));
#else
#endif
    }

    private void ParseInput()
    {
        int y;

        Func<int, int, Point> mappingFunction =  (_, _) => throw new PuzzleException("Mapping not set.");

        for (y = 0; y < Input.Length; y++)
        {
            var line = Input[y];

            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }

            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == ' ')
                {
                    continue;
                }

                if (x % FaceSize == 0 || y % FaceSize == 0)
                {
                    if (! _mappings.ContainsKey(new Point(x / FaceSize, y / FaceSize)))
                    {
                        Count();
                    }

                    mappingFunction = _mappings[new Point(x / FaceSize, y / FaceSize)];
                }

                if (_position == null && line[x] == '.')
                {
                    _position = new Point(mappingFunction(x % FaceSize, y % FaceSize));
                }

                var position = mappingFunction(x % FaceSize, y % FaceSize);

                _cube[position.X, position.Y, position.Z].Tile = line[x];

                _cube[position.X, position.Y, position.Z].InitialPosition = new Point(x, y);
            }
        }

        _path = Input[y + 1];

        Count();
    }

    private void WalkCube()
    {
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

                // TODO: Change direction - this depends on which face of cube :( ... maybe?
                _direction = (_direction.X, _direction.Y, _direction.Z, _path[i]) switch
                {
                    (1, 0, 0, 'R') => new Point(0, 1),
                    (0, 0, -1, 'L') => new Point(1, 0),
                    _ => throw new PuzzleException("Don't know how to turn.")
                };

                previous = i + 1;
            }
        }
    }

    private void Walk(int length)
    {
        while (length > 0)
        {
            var position = new Point(_position);

            position += _direction;

            var tile = GetElement(position).Tile;

            if (tile == '#')
            {
                return;
            }

            if (tile == '\0')
            {
                var newDirection = Wrap(position);

                position += newDirection;

                if (GetElement(position).Tile == '#')
                {
                    return;
                }

                _direction = newDirection;

                // This accounts for previous decrement taking us into '\0'.
//                length++;
            }

            _position = position;

            Console.WriteLine(GetElement(_position).InitialPosition);

            length--;
        }
    }

    private Point Wrap(Point position)
    {
        // Change direction. If hit #, return.
        // Should only be one option that isn't where you came from...
        // Don't change class variable if change goes to a #.

        return (position.X, position.Y, position.Z, _direction.X, _direction.Y, _direction.Z) switch
        {
            // Top ->
            //   Back
            (_, 0, FaceEndIndex, 0, -1, 0) => new Point(),
            //   Left
            (0, _, FaceEndIndex, 0, -1, 0) => new Point(),
            //   Right
            (FaceEndIndex, _, FaceEndIndex, 0, -1, 0) => new Point(),
            //   Front
            (_, FaceEndIndex, FaceEndIndex, _, 1, _) => new Point(0, 0, -1),
            // Front ->
            //   Right
            (FaceEndIndex, FaceEndIndex, _, 1, 0, 0) => new Point(0, -1),
            _ => throw new PuzzleException("Don't know how to wrap.")
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private (char Tile, Point InitialPosition) GetElement(Point position)
    {
        return _cube[position.X, position.Y, position.Z];
    }

    private void Count()
    {
        var c = 0;

        for (var x = 0; x < FaceEndIndex; x++)
        {
            for (var y = 0; y < FaceEndIndex; y++)
            {
                for (var z = 0; z < FaceEndIndex; z++)
                {
                    if (_cube[x, y, z].Tile != '\0')
                    {
                        c++;
                    }
                }
            }
        }

        Console.WriteLine(c);
    }
}