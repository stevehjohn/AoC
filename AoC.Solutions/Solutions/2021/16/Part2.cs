using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var rootPacket = GetRootPacket();

        var result = rootPacket.ProcessPacket();

        return result.ToString();
    }
}