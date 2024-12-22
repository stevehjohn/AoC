using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        foreach (var line in Input)
        {
            var sequence = GetSequence(long.Parse(line));
        }
        
        return "Unknown";
    }
    
    private static List<(long Price, long Delta)> GetSequence(long seed)
    {
        var previous = seed % 10;
        
        var sequence = new List<(long Price, long Delta)>();

        for (var i = 0; i < 1_999; i++)
        {
            seed = SimulateRound(seed);

            var price = seed % 10;
            
            sequence.Add((price, price - previous));

            previous = price;
        }

        return sequence;
    }
}