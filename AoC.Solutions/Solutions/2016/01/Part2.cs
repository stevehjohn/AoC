using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2016._01;

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

            lines.Add((sX, sY, x, y));

            var intersection = CheckForIntersection(lines);

            if (intersection != null)
            {
                return Math.Abs(intersection.Value.X) + Math.Abs(intersection.Value.Y);
            }
        }

        throw new PuzzleException("Solution not found.");
    }

    private static (int X, int Y)? CheckForIntersection(List<(int StartX, int StartY, int EndX, int EndY)> lines)
    {
        foreach (var line1 in lines)
        {
            foreach (var line2 in lines)
            {
                if (line1 == line2)
                {
                    continue;
                }

                if (line1.StartX == line1.EndX && line2.StartX == line2.EndX)
                {
                    continue;
                }

                if (line1.StartY == line1.EndY && line2.StartY == line2.EndY)
                {
                    continue;
                }

                if (line1.StartX == line1.EndX)
                {
                    if (Math.Min(line2.StartY, line2.EndY) > line1.StartX && Math.Max(line2.StartY, line2.EndY) < line1.StartX)
                    {
                        var x = line1.StartX;

                        var y = line2.StartY;
                    }
                }
                else
                {
                    if (Math.Min(line2.StartX, line2.EndX) > line1.StartY && Math.Max(line2.StartX, line2.EndX) < line1.StartY)
                    {
                        var x = line2.StartX;

                        var y = line1.StartY;
                    }
                }
            }
        }

        return null;
    }
}