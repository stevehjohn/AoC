using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = RunHashes();

        return result.ToString();
    }
}