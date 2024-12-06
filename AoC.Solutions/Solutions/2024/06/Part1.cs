using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var answer = WalkMap();

        return answer.ToString();
    }
}