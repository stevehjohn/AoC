using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        InitialiseBots();

        var result = RunBots(true);

        return result.ToString();
    }
}