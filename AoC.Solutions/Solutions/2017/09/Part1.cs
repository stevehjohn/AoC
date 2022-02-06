using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._09;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = CountGroupsAndGarbage(Input[0]);

        return result.Groups.ToString();
    }
}