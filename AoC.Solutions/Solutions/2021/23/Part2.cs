using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput(true);

        var result = Solve();

        return result.ToString();
    }
}