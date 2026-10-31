using System.Numerics;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._18;

public abstract class Base : Solution
{
    public override string Description => "Like a rogue";

    protected static int Solve(string state, int totalRows)
    {
        var width = state.Length;

        if (width > 128)
        {
            throw new ArgumentOutOfRangeException(nameof(state), "Maximum supported width is 128.");
        }

        ulong low = 0;
        
        ulong high = 0;

        for (var i = 0; i < width; i++)
        {
            if (state[i] != '^')
            {
                continue;
            }

            if (i < 64)
            {
                low |= 1UL << i;
            }
            else
            {
                high |= 1UL << (i - 64);
            }
        }

        var lowBits = Math.Min(width, 64);
        
        var highBits = Math.Max(0, width - 64);

        var lowMask = lowBits == 64
            ? ulong.MaxValue
            : (1UL << lowBits) - 1;

        var highMask = highBits == 64
            ? ulong.MaxValue
            : (1UL << highBits) - 1;

        var safe = 0;

        for (var row = 0; row < totalRows; row++)
        {
            safe += width - BitOperations.PopCount(low) - BitOperations.PopCount(high);

            var nextLow = ((low << 1) ^ (low >> 1 | high << 63)) & lowMask;

            var nextHigh = ((high << 1 | low >> 63) ^ (high >> 1)) & highMask;

            low = nextLow;
            
            high = nextHigh;
        }

        return safe;
    }
}