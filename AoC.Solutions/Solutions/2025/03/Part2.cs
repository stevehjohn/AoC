using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var joltage = FindHighestJoltage(12);

        return joltage.ToString();
    }
}