using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var discs = ParseInput();

        discs.Add((7, 11, 0));

        var result = Solve(discs);

        return result.ToString();
    }
}