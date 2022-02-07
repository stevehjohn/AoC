using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = RunDance(Input[0]);

        return result;
    }
}