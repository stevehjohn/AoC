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
        
        return total.ToString();    }

    private long GetHistory(List<List<long>> sequences)
    {
        for (var i = sequences.Count - 2; i >= 0; i--)
        {
            sequences[i].Insert(0, sequences[i].First() - sequences[i + 1]. First());
        }
        
        return sequences[0].First();
    }
}