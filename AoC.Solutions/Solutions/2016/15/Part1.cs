using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var discs = ParseInput();

        var result = Solve(discs);

        return result.ToString();
    }
}