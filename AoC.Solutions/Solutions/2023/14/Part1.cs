using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        MoveRocks(0, -1);

        var result = GetLoad();

        return result.ToString();
    }
}