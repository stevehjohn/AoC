using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._03;

public class Line
{
    public Point Start { get; init; }

    public Point End { get; init; }

    private int XMin => Math.Min(Start.X, End.X);

    private int XMax => Math.Max(Start.X, End.X);

    private int YMin => Math.Min(Start.Y, End.Y);

    private int YMax => Math.Max(Start.Y, End.Y);

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
        if (XMin >= line.XMin && XMin <= line.XMax && line.YMin >= YMin && line.YMin <= YMax)
        {
            return new Point(XMin, line.YMin);
        }

        if (line.XMin >= XMin && line.XMin <= XMax && YMin >= line.YMin && YMin <= line.YMax)
        {
            return new Point(line.XMin, YMin);
        }

        return null;
    }
}