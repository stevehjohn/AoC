using System.Numerics;

namespace AoC.Solutions.Libraries;

public static class Measurement
{
    public static T GetManhattanDistance<T>(T x1, T y1, T x2, T y2) where T : INumber<T>
    {
        return Maths.Abs(x1 - x2) + Maths.Abs(y1 - y2);
    }
}