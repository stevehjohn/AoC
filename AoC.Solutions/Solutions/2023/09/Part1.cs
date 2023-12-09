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

            total += GetHistory(sequences, (s, l) => s[^1] + l);
        }
        
        return total.ToString();
    }
}