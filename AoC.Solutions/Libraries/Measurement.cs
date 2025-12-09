using System.Numerics;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Libraries;

public static class Measurement
{
    public static T GetManhattanDistance<T>(T x1, T y1, T x2, T y2) where T : INumber<T>
    {
        return Maths.Abs(x1 - x2) + Maths.Abs(y1 - y2);
    }

    public static double GetDistance(Vertex left, Vertex right)
    {
        var distance = Math.Pow(Math.Abs(left.X - right.X), 2);
        
        distance += Math.Pow(Math.Abs(left.Y - right.Y), 2);
        
        distance += Math.Pow(Math.Abs(left.Z - right.Z), 2);

        distance = Math.Sqrt(distance);

        return distance;
    }

    public static long AreaInCells(Coordinate left, Coordinate right)
    {
        return (Math.Abs(left.X - right.X) + 1) * (Math.Abs(left.Y - right.Y) + 1);
    }
}