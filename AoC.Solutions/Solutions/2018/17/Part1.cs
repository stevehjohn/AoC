using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._17;

[UsedImplicitly]
public class Part1 : Base
{
    private const int SpringX = 500;

    private char[,] _map;

    public override string GetAnswer()
    {
        ParseInput();

        return "TESTING";
    }

    private void ParseInput()
    {
        var boundaries = FindBoundaries();

        _map = new char[boundaries.XMax - boundaries.XMin + 1, boundaries.YMax - boundaries.YMin + 1];

        foreach (var line in Input)
        {
            var data = ParseLine(line);

            for (var i = data.RangeStart; i <= data.RangeEnd; i++)
            {
                if (data.RangeAxis == 'x')
                {
                    _map[i - boundaries.XMin, data.Single - boundaries.YMin] = '#';
                }
                else
                {
                    _map[data.Single - boundaries.XMin, i - boundaries.YMin] = '#';
                }
            }
        }
    }

    private (int XMin, int XMax, int YMin, int YMax) FindBoundaries()
    {
        var xMin = int.MaxValue;

        var yMin = int.MaxValue;

        var xMax = int.MinValue;

        var yMax = int.MinValue;

        foreach (var line in Input)
        {
            var data = ParseLine(line);

            if (data.RangeAxis == 'y')
            {
                xMin = Math.Min(data.Single, xMin);

                xMax = Math.Max(data.Single, xMax);

                yMin = Math.Min(data.RangeStart, yMin);

                yMax = Math.Max(data.RangeEnd, yMax);
            }
            else
            {
                xMin = Math.Min(data.RangeStart, xMin);

                xMax = Math.Max(data.RangeEnd, xMax);

                yMin = Math.Min(data.Single, yMin);

                yMax = Math.Max(data.Single, yMax);
            }
        }

        return (xMin, xMax, yMin, yMax);
    }

    private static (int Single, int RangeStart, int RangeEnd, char RangeAxis) ParseLine(string line)
    {
        var halves = line.Split(',', StringSplitOptions.TrimEntries);

        var rangeAxis = halves[1][0];

        var single = int.Parse(halves[0][2..]);

        var range = halves[1][2..].Split("..", StringSplitOptions.TrimEntries);

        var rangeStart = Math.Min(int.Parse(range[0]), int.Parse(range[1]));
        
        var rangeEnd = Math.Max(int.Parse(range[0]), int.Parse(range[1]));

        return (single, rangeStart, rangeEnd, rangeAxis);
    }
}