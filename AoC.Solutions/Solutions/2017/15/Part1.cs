using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var generatorA = long.Parse(Input[0].Split(' ')[^1]);
        
        var generatorB = long.Parse(Input[1].Split(' ')[^1]);

        const long modulus = int.MaxValue;

        var matches = 0;

        for (var i = 0; i < 40_000_000; i++)
        {
            var productA = generatorA * 16807;
            
            generatorA = (productA & modulus) + (productA >> 31);

            if (generatorA >= modulus)
            {
                generatorA -= modulus;
            }

            var productB = generatorB * 48271;
            
            generatorB = (productB & modulus) + (productB >> 31);

            if (generatorB >= modulus)
            {
                generatorB -= modulus;
            }

            if ((generatorA & 0xFFFF) == (generatorB & 0xFFFF))
            {
                matches++;
            }
        }

        return matches.ToString();
    }
}