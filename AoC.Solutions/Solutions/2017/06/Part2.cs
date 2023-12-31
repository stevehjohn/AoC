using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._06;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        RunUntilRepeats();

        var result = RunUntilRepeats() - 1;

        return result.ToString();
    }
}