using System.Numerics;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var lookup = new long[24];

        for (var bit = 0; bit < 24; bit++)
        {
            var number = 1L << bit;

            for (var i = 0; i < 2_000; i++)
            {
                number = SimulateRound(number);
            }

            lookup[bit] = number;
        }

        var result = 0L;

        foreach (var line in Input)
        {
            var seed = (uint) (long.Parse(line) & 0xFFFFFF);

            var number = 0L;

            while (seed != 0)
            {
                var bit = BitOperations.TrailingZeroCount(seed);

                number ^= lookup[bit];

                seed &= seed - 1;
            }

            result += number;
        }

        return result.ToString();
    }
}