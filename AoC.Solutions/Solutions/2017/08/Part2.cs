using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = RunProgram(true);

        return result.ToString();
    }
}