using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._05;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var steps = Run();

        return steps.ToString();
    }
}