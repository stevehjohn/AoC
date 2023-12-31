using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = Solve(Input[0], 40);

        return result.ToString();
    }
}