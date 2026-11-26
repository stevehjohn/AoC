using System.Numerics;
using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._25;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var parts = Input[0].Split([' ', ',', '.'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var row = int.FastParse(parts[15]);

        var column = int.FastParse(parts[17]);

        var diagonal = (long) row + column - 2;

        var exponent = diagonal * (diagonal + 1) / 2 + column - 1;

        const long modulus = 33554393;

        var code = 20151125 * BigInteger.ModPow(252533, exponent, modulus) % modulus;

        return code.ToString();
    }
}