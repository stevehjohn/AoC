#define TEST
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._22;

public class Part2 : Base
{
#if TEST
    private const int FaceSize = 4;
#else
    private const int FaceSize = 50;
#endif

    private readonly char[,,] _cube = new char[FaceSize, FaceSize, FaceSize];

    private string _path;

    private readonly Dictionary<Point, object> _mappings = new();

    public override string GetAnswer()
    {
        InitialiseMappings();

        ParseInput();

        return "";
    }

    private void InitialiseMappings()
    {
#if TEST
        _mappings.Add(new Point(2, 0), null);
#else
#endif
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

            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '\0')
                {
                    continue;
                }

                var mapping = _mappings[new Point(x / FaceSize, y / FaceSize)];
            }
        }

        _path = Input[y + 1];
    }
}