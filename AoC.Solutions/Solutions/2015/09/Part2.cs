using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._09;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = Solve().Longest;

        return result.ToString();
    }
}