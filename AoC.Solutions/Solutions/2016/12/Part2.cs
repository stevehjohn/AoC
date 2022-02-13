using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = RunProgram(1);

        return result.ToString();
    }
}