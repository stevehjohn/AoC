using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._23;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = Solve();

        return result.ToString();
    }
}