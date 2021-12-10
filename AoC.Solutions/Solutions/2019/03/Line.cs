using AoC.Common;

namespace AoC.Solutions._2019._03;

public class Line
{
    public Point Start { get; set; }

    public Point End { get; set; }

    public bool Intersects(Point point)
    {
        if (Start.X == End.X)
        {
            return point.X == Start.X
                   && point.Y >= Math.Min(Start.Y, End.Y)
                   && point.Y <= Math.Max(Start.Y, End.Y);
        }

        return point.Y == Start.Y
               && point.X >= Math.Min(Start.X, End.X)
               && point.X <= Math.Max(Start.X, End.X);
    }

    public Point IntersectionPoint(Line line)
    {
        if (Start.X == End.X)
        {
            if (line.Start.X == line.End.X)
            {
                return null;
            }

            if (Start.X >= Math.Min(line.Start.X, line.End.X)
                && Start.X <= Math.Max(line.Start.X, line.End.X)
                && line.Start.Y >= Math.Min(Start.Y, End.Y)
                && line.Start.Y <= Math.Max(Start.Y, End.Y))
            {
                return new Point(Start.X, line.Start.Y);
            }
        }
        else
        {
            if (line.Start.Y == line.End.Y)
            {
                return null;
            }

            if (line.Start.X >= Math.Min(Start.X, End.X)
                && line.Start.X <= Math.Max(Start.X, End.X)
                && Start.Y >= Math.Min(line.Start.Y, line.End.Y)
                && Start.Y <= Math.Max(line.Start.Y, line.End.Y))
            {
                return new Point(line.Start.X, Start.Y);
            }
        }

        return null;
    }
}