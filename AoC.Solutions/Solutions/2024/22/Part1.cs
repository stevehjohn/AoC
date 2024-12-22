using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = 0L;

        foreach (var line in Input)
        {
            result += SimulateBuyer(long.Parse(line));
        }

        return result.ToString();
    }

    private static long SimulateBuyer(long seed)
    {
        var number = seed;
        
        for (var i = 0; i < 2_000; i++)
        {
            number = SimulateRound(number);
        }

        return number;
    }
}