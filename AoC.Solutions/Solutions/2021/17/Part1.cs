using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._17;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        Simulate();

        return HighestY.ToString();
    }
}