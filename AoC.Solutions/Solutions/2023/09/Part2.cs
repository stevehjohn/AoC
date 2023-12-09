using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._09;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var total = 0L;

        for (var i = 0; i < Sequences.Count; i++)
        {
            var sequences = Extrapolate(Sequences[i]);

            total += GetHistory(sequences, (s, l) => s[0] - l);
        }

        return total.ToString();
    }
}