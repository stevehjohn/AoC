using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._09;

[UsedImplicitly]
public class Part1 : Base
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

    private static long GetHistory(List<List<long>> sequences)
    {
        for (var i = sequences.Count - 2; i >= 0; i--)
        {
            sequences[i].Add(sequences[i].Last() + sequences[i + 1]. Last());
        }
        
        return sequences[0].Last();
    }
}