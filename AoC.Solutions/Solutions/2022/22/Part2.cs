#define TEST
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

    private readonly char[,,] _cube = new char[FaceSize + 2, FaceSize + 2, FaceSize + 2];

    private string _path;

    private readonly Dictionary<Point, Func<int, int, Point>> _mappings = new();

    private Point _position;

    public override string GetAnswer()
    {
        InitialiseMappings();

        ParseInput();

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

                _cube[position.X, position.Y, position.Z] = line[x];
            }
        }

        _path = Input[y + 1];

        Count();
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
                    if (_cube[x, y, z] != '\0')
                    {
                        c++;
                    }
                }
            }
        }

        Console.WriteLine(c);
    }
}