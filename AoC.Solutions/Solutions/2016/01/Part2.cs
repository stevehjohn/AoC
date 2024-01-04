using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._01;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = GetDistance();

        return result.ToString();
    }

    private int GetDistance()
    {
        var steps = Input[0].Split(", ", StringSplitOptions.TrimEntries);

        var x = 0;

        var y = 0;

        var dX = 0;

        var dY = -1;

        var lines = new List<(int StartX, int StartY, int EndX, int EndY)>();

        foreach (var step in steps)
        {
            if (step[0] == 'R')
            {
                (dX, dY) = (-dY, dX);
            }
            else
            {
                (dX, dY) = (dY, -dX);
            }

            var amount = int.Parse(step[1..]);

            var sX = x;

            var sY = y;

            x += dX * amount;

            y += dY * amount;

            var newLine = (sX, sY, x, y);

            var intersection = CheckForIntersection(lines, newLine);

            if (intersection != null)
            {
                return Math.Abs(intersection.Value.X) + Math.Abs(intersection.Value.Y);
            }

            lines.Add(newLine);
        }

        throw new PuzzleException("Solution not found.");
    }

    private static (int X, int Y)? CheckForIntersection(List<(int StartX, int StartY, int EndX, int EndY)> lines, (int StartX, int StartY, int EndX, int EndY) newLine)
    {
        foreach (var line in lines)
        {
            if (line == newLine)
            {
                continue;
            }

            if (line.StartX == line.EndX && newLine.StartX == newLine.EndX)
            {
                continue;
            }

            if (line.StartY == line.EndY && newLine.StartY == newLine.EndY)
            {
                continue;
            }

            if (line.StartX == line.EndX)
            {
                if (newLine.StartY > Math.Min(line.StartY, line.EndY)
                    && newLine.StartY < Math.Max(line.StartY, line.EndY)
                    && Math.Min(newLine.StartX, newLine.EndX) < line.StartX
                    && Math.Max(newLine.StartX, newLine.EndX) > line.StartX)
                {
                    var x = newLine.StartY;

                    var y = line.StartX;

                    return (x, y);
                }
            }
            else
            {
                if (newLine.StartX > Math.Min(line.StartX, line.EndX)
                    && newLine.StartX < Math.Max(line.StartX, line.EndX)
                    && Math.Min(newLine.StartY, newLine.EndY) < line.StartY
                    && Math.Max(newLine.StartY, newLine.EndY) > line.StartY)
                {
                    var x = newLine.StartY;

                    var y = line.StartX;

                    return (x, y);
                }
            }
        }

        return null;
    }
}