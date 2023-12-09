using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._09;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var total = 0L;

        for (var i = 0; i < Sequences.Count; i++)
        {
            var sequences = Extrapolate(Sequences[i]);

            total += GetHistory(sequences, (s, l) => s[^1] + l);
        }

        return total.ToString();
    }
}