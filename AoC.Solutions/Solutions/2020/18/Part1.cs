using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = 0L;

        foreach (var line in Input)
        {
            var operations = ParseLine("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2");

            result += CalculateResult(operations);
        }

        return result.ToString();
    }
}