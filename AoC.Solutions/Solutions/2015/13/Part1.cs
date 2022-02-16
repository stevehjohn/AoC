using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._13;

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