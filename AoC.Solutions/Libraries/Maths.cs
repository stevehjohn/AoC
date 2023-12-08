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

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static long GreatestCommonFactor(long left, long right)
    {
        while (left != 0 && right != 0)
        {
            if (left > right)
            {
                left %= right;
            }
            else
            {
                right %= left;
            }
        }

        return left | right;
    }
}