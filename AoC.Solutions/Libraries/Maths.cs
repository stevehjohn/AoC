using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Solutions.Libraries;

public static class Maths
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static long LowestCommonMultiple(List<long> input)
    {
        var queue = new Queue<long>(input.Count * 2);

        foreach (var item in input)
        {
            queue.Enqueue(item);
        }
        
        while (true)
        {
            long left;
            
            long right;
            
            if (queue.Count == 2)
            {
                left = queue.Dequeue();

                right = queue.Dequeue();

                return left * right / GreatestCommonFactor(left, right);
            }

            left = queue.Dequeue();

            right = queue.Dequeue();

            var lowestCommonMultiple = left * right / GreatestCommonFactor(left, right);

            queue.Enqueue(lowestCommonMultiple);
        }
    }

    public static long LowestCommonMultiple(long left, long right)
    {
        return left * right / GreatestCommonFactor(left, right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static long GreatestCommonFactor(long left, long right)
    {
        var gcdExponentOnTwo = BitOperations.TrailingZeroCount(left | right);

        left >>= gcdExponentOnTwo;
        
        right >>= gcdExponentOnTwo;

        while (left != right)
        {
            if (left < right)
            {
                right -= left;

                right >>= BitOperations.TrailingZeroCount(right);
            }
            else
            {
                left -= right;

                left >>= BitOperations.TrailingZeroCount(left);
            }
        }

        return left << gcdExponentOnTwo;
    }
}