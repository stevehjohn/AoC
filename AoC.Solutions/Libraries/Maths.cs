using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Solutions.Libraries;

public static class Maths
{
    public static T Abs<T>(T number) where T : INumber<T>
    {
        return number < default(T) ? -number : number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long LowestCommonMultiple(IEnumerable<long> input)
    {
        return input.Aggregate(1L, (lcm, n) =>
        {
            var gcd = GreatestCommonFactor(lcm, n);
            
            return lcm / gcd * n;
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long GreatestCommonFactor(long left, long right)
    {
        while (right != 0)
        {
            (left, right) = (right, left % right);
        }

        return left;
    }
}