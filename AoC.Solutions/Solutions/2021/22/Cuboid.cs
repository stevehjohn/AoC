#define DUMP
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._22;

public class Cuboid
{
    public Point A { get; }

    public Point B { get; }

    public int Volume => (B.X - A.X + 1) * (B.Y - A.Y + 1) * (B.Z - A.Z + 1);

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

//    public Cuboid Intersects(Cuboid other)
//    {
//        var aX = LineIntersect(A.X, B.X, other.A.X, other.B.X);

//        var aY = LineIntersect(A.Y, B.Y, other.A.Y, other.B.Y);

//        var aZ = LineIntersect(A.Z, B.Z, other.A.Z, other.B.Z);

//        var bX = LineIntersect(other.A.X, other.B.X, A.X, B.X);

//        var bY = LineIntersect(other.A.Y, other.B.Y, A.Y, B.Y);

//        var bZ = LineIntersect(other.A.Z, other.B.Z, A.Z, B.Z);

//        if (aX == null || aY == null || aZ == null || bX == null || bY == null || bZ == null)
//        {
//            return null;
//        }

//        var intersection = new Cuboid(new Point(Math.Min(aX.Value, bX.Value), Math.Min(aY.Value, bY.Value), Math.Min(aZ.Value, bZ.Value)),
//                                      new Point(Math.Max(aX.Value, bX.Value), Math.Max(aY.Value, bY.Value), Math.Max(aZ.Value, bZ.Value)));

//#if DUMP && DEBUG
//        Console.WriteLine($"{A} -> {B}");
//        Console.WriteLine($"{other.A} -> {other.B}");
//        Console.WriteLine($"{intersection.A} -> {intersection.B}");
//        Console.WriteLine();
//#endif

//        return intersection;
//    }

//    private static int? LineIntersect(int aLeft, int aRight, int bLeft, int bRight)
//    {
//        if (bLeft >= aLeft && bLeft <= aRight)
//        {
//            return bLeft;
//        }

//        if (bRight >= aLeft && bRight <= aRight)
//        {
//            return bRight;
//        }

//        return null;
//    }
}