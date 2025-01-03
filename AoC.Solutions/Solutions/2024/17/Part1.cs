using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._17;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = RunProgram();

        return string.Join(',', result);
    }
}