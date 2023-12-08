using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Solutions.Libraries;

public static class Maths
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static T LowestCommonMultiple<T>(List<T> input) where T : INumber<T>
    {
        var queue = new Queue<T>(input.Count * 2);

        foreach (var item in input)
        {
            queue.Enqueue(item);
        }
        
        while (true)
        {
            T left;
            
            T right;
            
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

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static T GreatestCommonFactor<T>(T left, T right) where T : INumber<T>
    {
        while (right != default)
        {
            left %= right;

            if (left == default)
            {
                return right;
            }

            right %= left;
        }

        return left;
    }
}