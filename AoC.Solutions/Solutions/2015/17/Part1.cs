using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._17;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var containers = Input.Select(int.Parse).ToArray();

        var combinations = 1 << containers.Length;
        
        var sums = new int[combinations];

        var count = 0;

        for (var mask = 1; mask < combinations; mask++)
        {
            var previous = mask & (mask - 1);
            
            var bit = mask ^ previous;

            var index = System.Numerics.BitOperations.TrailingZeroCount((uint)bit);

            var sum = sums[previous] + containers[index];

            sums[mask] = sum;

            if (sum == 150)
            {
                count++;
            }
        }

        return count.ToString();
    }
}