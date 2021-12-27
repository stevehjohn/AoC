﻿using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._22;

public class Cuboid
{
    public Point A { get; }

    public Point B { get; }

    public int Volume
    {
        get { return (B.X - A.X + 1) * (B.Y - A.Y + 1) * (B.Z - A.Z + 1); }
    }

    public Cuboid(Point a, Point b)
    {
        A = a;
        B = b;
    }

    public Cuboid Intersects(Cuboid other)
    {
        var aX = LineIntersect(A.X, B.X, other.A.X);

        var aY = LineIntersect(A.Y, B.Y, other.A.Y);

        var aZ = LineIntersect(A.Z, B.Z, other.A.Z);

        var bX = LineIntersect(other.A.X, other.B.X, B.X);

        var bY = LineIntersect(other.A.Y, other.B.Y, B.Y);

        var bZ = LineIntersect(other.A.Z, other.B.Z, B.Z);

        if (aX == null || aY == null || aZ == null || bX == null || bY == null || bZ == null)
        {
            return null;
        }

        return new Cuboid(new Point(aX.Value, aY.Value, aZ.Value), new Point(bX.Value, bY.Value, bZ.Value));
    }

    private static int? LineIntersect(int aLeft, int aRight, int b)
    {
        if (b >= aLeft && b <= aRight)
        {
            return b;
        }

        return null;
    }
}