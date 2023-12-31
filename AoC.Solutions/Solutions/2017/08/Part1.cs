using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._08;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = RunProgram();

        return result.ToString();
    }
}