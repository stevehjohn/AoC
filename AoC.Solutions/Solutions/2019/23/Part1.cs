using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._23;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        return RunNetwork().ToString();
    }
}