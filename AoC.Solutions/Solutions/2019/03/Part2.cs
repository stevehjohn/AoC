using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var intersections = GetIntersections();

        var steps = new List<int>();

        foreach (var intersection in intersections)
        {
            steps.Add(GetSteps(intersection));
        }

        return steps.Min().ToString();
    }

    private int GetSteps(Point intersection)
    {
        return WalkTo(Path1, intersection) + WalkTo(Path2, intersection);
    }

    private static int WalkTo(List<Line> lines, Point target)
    {
        var steps = 0;

        foreach (var line in lines)
        {
            if (line.Intersects(target))
            {
                steps += Math.Abs(line.Start.X - target.X) + Math.Abs(line.Start.Y - target.Y);

                return steps;
            }

            steps += Math.Abs(line.End.X - line.Start.X) + Math.Abs(line.End.Y - line.Start.Y);
        }

        return int.MaxValue;
    }
}