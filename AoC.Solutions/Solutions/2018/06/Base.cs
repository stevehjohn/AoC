using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._06;

public abstract class Base : Solution
{
    public override string Description => "Safe coordinates";

    protected readonly List<Point> Points = new();

    protected int Width;

    protected int Height;

    protected static int ManhattanDistance(Point left, Point right)
    {
        return Math.Abs(left.X - right.X) + Math.Abs(left.Y - right.Y);
    }

    protected void ParseInput()
    {
        var points = new List<Point>();

        foreach (var line in Input)
        {
            points.Add(Point.Parse(line));
        }

        var xMin = points.Min(p => p.X);

        var yMin = points.Min(p => p.Y);

        foreach (var point in points)
        {
            Points.Add(new Point(point.X - xMin, point.Y - yMin));
        }

        Width = Points.Max(p => p.X) + 1;

        Height = Points.Max(p => p.Y) + 1;
    }
}