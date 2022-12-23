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

    private const int FaceEndIndex = FaceSize - 1;

    private readonly char[,,] _cube = new char[FaceSize, FaceSize, FaceSize];

    private string _path;

    private readonly Dictionary<Point, (Point Start, Func<Point, Point> XChanged, Func<Point, Point> YChanged)> _mappings = new();

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
        _mappings.Add(new Point(2, 0), (new Point(0, 0, FaceEndIndex), p => new Point(p.X + 1, p.Y, p.Z), p => new Point(p.X - FaceSize, p.Y + 1, p.Z)));
#else
#endif
    }

    private void ParseInput()
    {
        int y;

        Point cubePosition = null;

        (Point Start, Func<Point, Point> XChanged, Func<Point, Point> YChanged) mapping = (new Point(-1, -1, -1), _ => throw new PuzzleException("Mapping not found."), _ => throw new PuzzleException("Mapping not found."));

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

                if (x % FaceSize == 0 && y % FaceSize == 0)
                {
                    mapping = _mappings[new Point(x / FaceSize, y / FaceSize)];

                    cubePosition = new Point(mapping.Start);
                }

                if (_position == null && line[x] == '.')
                {
                    _position = new Point(cubePosition);
                }

                // ReSharper disable once PossibleNullReferenceException - want this to fail if null.
                _cube[cubePosition.X, cubePosition.Y, cubePosition.Z] = line[x];

                cubePosition = mapping.XChanged(cubePosition);
            }

            // ReSharper disable once PossibleNullReferenceException - want this to fail if null.
            cubePosition = mapping.YChanged(cubePosition);
        }

        _path = Input[y + 1];
    }
}