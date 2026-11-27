using System.Numerics;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._25;

[UsedImplicitly]
public class Part1 : Base
{
    private const long Modulus = 20201227;

    private const long Subject = 7;

    public override string GetAnswer()
    {
        var pk1 = long.Parse(Input[0]);

        var pk2 = long.Parse(Input[1]);

        var loopSize = GetLoopSize(pk1);

        return BigInteger.ModPow(pk2, loopSize, Modulus).ToString();
    }

    private static long GetLoopSize(long key)
    {
        const int m = 4495;

        var babies = new Dictionary<long, int>(m);

        var value = 1L;

        for (var j = 0; j < m; j++)
        {
            babies.TryAdd(value, j);

            value = value * Subject % Modulus;
        }

        var inverse = (long) BigInteger.ModPow(value, Modulus - 2, Modulus);

        var giant = key;

        for (var i = 0; i < m; i++)
        {
            if (babies.TryGetValue(giant, out var j))
            {
                return (long) i * m + j;
            }

            giant = giant * inverse % Modulus;
        }

        throw new InvalidOperationException("No loop size found.");
    }
}