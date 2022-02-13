using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._14;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = RunHashes(2016);

        return result.ToString();
    }
}