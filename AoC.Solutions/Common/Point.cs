namespace AoC.Common;

public class Point
{
    public int X { get; set; }

    public int Y { get; set; }

    public Point(Point point)
    {
        X = point.X;

        Y = point.Y;
    }

    public Point(int x, int y)
    {
        X = x;

        Y = y;
    }
}