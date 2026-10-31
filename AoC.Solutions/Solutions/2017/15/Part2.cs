using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var generatorA = uint.Parse(Input[0].AsSpan(
            Input[0].LastIndexOf(' ') + 1));

        var generatorB = uint.Parse(Input[1].AsSpan(
            Input[1].LastIndexOf(' ') + 1));

        var matches = 0;

        for (var i = 0; i < 5_000_000; i++)
        {
            do
            {
                generatorA = Next(generatorA, 16807);
            } while ((generatorA & 3) != 0);

            do
            {
                generatorB = Next(generatorB, 48271);
            } while ((generatorB & 7) != 0);

            if ((ushort) generatorA == (ushort) generatorB)
            {
                matches++;
            }
        }

        return matches.ToString();
    }

    private static uint Next(uint value, uint factor)
    {
        const ulong modulus = 2147483647;

        var product = (ulong) value * factor;

        // ReSharper disable once UselessBinaryOperation - Rider's full of shit sometimes.
        var result = (product & modulus) + (product >> 31);

        return (uint) (result >= modulus
            ? result - modulus
            : result);
    }
}