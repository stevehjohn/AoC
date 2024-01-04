using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._22;

public class Cuboid
{
    private Point A { get; }

    private Point B { get; }

    public long Volume => ((long) B.X - A.X + 1) * ((long) B.Y - A.Y + 1) * ((long) B.Z - A.Z + 1);

    public Cuboid(Point a, Point b)
    {
        A = a;
        B = b;
    }

    public Cuboid Intersects(Cuboid other)
    {
        var iX = GetIntersection(A.X, B.X, other.A.X, other.B.X);

        var iY = GetIntersection(A.Y, B.Y, other.A.Y, other.B.Y);
        
        var iZ = GetIntersection(A.Z, B.Z, other.A.Z, other.B.Z);

        if (iX == null || iY == null || iZ == null)
        {
            return null;
        }

        var intersection = new Cuboid(new Point(iX.Value.A, iY.Value.A, iZ.Value.A), new Point(iX.Value.B, iY.Value.B, iZ.Value.B));

        return intersection;
    }

    private static (int A, int B)? GetIntersection(int aL, int bL, int aR, int bR)
    {
        if (aL <= bR && bL >= aR)
        {
            return (Math.Max(aL, aR), Math.Min(bL, bR));
        }

        return null;
    }
}