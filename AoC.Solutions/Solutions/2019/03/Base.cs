using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._03;

public abstract class Base : Solution
{
    public override string Description => "Path intersections";

    protected List<Line> Path1;

    protected List<Line> Path2;

    protected List<Point> GetIntersections()
    {
        Path1 = ParseInput(Input[0]);

        Path2 = ParseInput(Input[1]);

        var intersections = new List<Point>();

        foreach (var line1 in Path1)
        {
            foreach (var line2 in Path2)
            {
                var intersectionPoint = line1.IntersectionPoint(line2);

                if (intersectionPoint != null)
                {
                    intersections.Add(intersectionPoint);
                }
            }
        }

        return intersections.Where(i => i.X != 0 || i.Y != 0).ToList();
    }

    private static List<Line> ParseInput(string input)
    {
        var lines = new List<Line>();

        var directions = input.Split(',', StringSplitOptions.TrimEntries);

        var position = new Point(0, 0);

        foreach (var direction in directions)
        {
            var newPoint = new Point(position);

            var distance = int.Parse(direction[1..]);

            switch (direction[0])
            {
                case 'U':
                    newPoint.Y += distance;
                    break;
                case 'D':
                    newPoint.Y -= distance;
                    break;
                case 'L':
                    newPoint.X -= distance;
                    break;
                case 'R':
                    newPoint.X += distance;
                    break;
            }

            lines.Add(new Line
                      {
                          Start = new Point(position),
                          End = new Point(newPoint)
                      });

            position = newPoint;
        }

        return lines;
    }
}