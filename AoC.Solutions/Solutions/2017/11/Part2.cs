using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._11;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var furthest = WalkPath(Input[0]).Furthest;

        return furthest.ToString();
    }
}