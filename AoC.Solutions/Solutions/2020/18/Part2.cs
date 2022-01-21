using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = 0L;

        foreach (var line in Input)
        {
            var operations = ParseLineToReverePolish(line, true);

            result += CalculateResult(operations);
        }

        return result.ToString();
    }
}