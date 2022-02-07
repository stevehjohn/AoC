using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var generatorA = long.Parse(Input[0].Split(' ')[^1]);

        var generatorB = long.Parse(Input[1].Split(' ')[^1]);
        
        var matches = 0;

        for (var i = 0; i < 40_000_000; i++)
        {
            generatorA = generatorA * 16807 % int.MaxValue;

            generatorB = generatorB * 48271 % int.MaxValue;

            if ((generatorA & 65_535) == (generatorB & 65_535))
            {
                matches++;
            }
        }

        return matches.ToString();
    }
}