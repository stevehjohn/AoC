using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 10_000_000; i++)
        {
            PerformMove();
        }

        return "TESTING";
    }
}