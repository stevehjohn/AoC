using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._20;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        (int Lows, int Highs) total = (0, 0);

        for (var i = 0; i < 1_000; i++)
        {
            var result = SendPulses();

            total.Lows += result.Lows;

            total.Highs += result.Highs;
        }

        return (total.Lows * total.Highs).ToString();
    }
}