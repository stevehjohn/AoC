using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput(true);

        var validCount = 0;

        foreach (var message in Messages)
        {
            validCount += RootRule.IsValid(message) ? 1 : 0;
        }

        return validCount.ToString();
    }
}