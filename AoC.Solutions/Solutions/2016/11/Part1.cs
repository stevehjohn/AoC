using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var state = ParseInput();

        var result = Solve(state);

        return result.ToString();
    }
}