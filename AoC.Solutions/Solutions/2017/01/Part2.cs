using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._01;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        return Solve(Input[0].Length / 2).ToString();
    }
}