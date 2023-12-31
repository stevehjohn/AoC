using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = Solve(Input[0], true);

        return result;
    }
}