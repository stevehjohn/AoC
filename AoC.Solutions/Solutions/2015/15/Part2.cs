using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = Solve(500);

        return result.ToString();
    }
}