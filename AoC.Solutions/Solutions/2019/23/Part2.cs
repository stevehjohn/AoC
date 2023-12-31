using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        return RunNetwork(true).ToString();
    }
}