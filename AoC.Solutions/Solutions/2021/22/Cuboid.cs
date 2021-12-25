using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._22;

public class Cuboid
{
    public Point A { get; }

    public Point B { get; }

    public Cuboid(Point a, Point b)
    {
        A = a;
        B = b;
    }

    public Cuboid Intersects(Cuboid other)
    {
        return null;
    }
}