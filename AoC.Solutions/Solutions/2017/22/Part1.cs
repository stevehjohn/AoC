using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var infections = 0;

        for (var i = 0; i < 10_000; i++)
        {
            infections += RunCycle() ? 1 : 0;
        }

        return infections.ToString();
    }
}