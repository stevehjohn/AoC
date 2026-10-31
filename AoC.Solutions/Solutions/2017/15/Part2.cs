using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var generatorA = long.Parse(Input[0].Split(' ')[^1]);
        
        var generatorB = long.Parse(Input[1].Split(' ')[^1]);

        var matches = 0;

        for (var i = 0; i < 5_000_000; i++)
        {
            do
            {
                generatorA = Next(generatorA, 16807);
            }
            while ((generatorA & 3) != 0);

            do
            {
                generatorB = Next(generatorB, 48271);
            }
            while ((generatorB & 7) != 0);

            if ((generatorA & 0xFFFF) == (generatorB & 0xFFFF))
            {
                matches++;
            }
        }

        return matches.ToString();
    }

    private static long Next(long value, long factor)
    {
        const long modulus = int.MaxValue;

        var product = value * factor;

        var result = (product & modulus) + (product >> 31);

        return result >= modulus ? result - modulus : result;
    }
}