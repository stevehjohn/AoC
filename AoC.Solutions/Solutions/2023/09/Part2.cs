using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._09;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var total = 0L;
        
        foreach (var sequence in Sequences)
        {
            var sequences = Extrapolate(sequence);

            total += GetHistory(sequences);
        }
        
        return total.ToString();
    }

    private static long GetHistory(List<long[]> sequences)
    {
        var last = 0L;
        
        for (var i = sequences.Count - 2; i >= 0; i--)
        {
            last = sequences[i][0] - last;
        }
        
        return last;
    }
}