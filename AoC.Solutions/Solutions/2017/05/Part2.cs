using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var steps = Run(true);

        return steps.ToString();
    }
}