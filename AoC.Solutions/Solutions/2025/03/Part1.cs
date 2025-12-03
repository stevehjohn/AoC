using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var joltage = FindHighestJoltage();

        return joltage.ToString();
    }
}