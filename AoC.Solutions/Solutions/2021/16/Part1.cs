using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var rootPacket = GetRootPacket();

        var versionSum = rootPacket.Version;

        versionSum += SumSubPacketVersions(rootPacket.SubPackets);
     
        return versionSum.ToString();
    }

    private static int SumSubPacketVersions(List<Packet> subPackets)
    {
        var sum = 0;

        foreach (var packet in subPackets)
        {
            sum += packet.Version;

            sum += SumSubPacketVersions(packet.SubPackets);
        }

        return sum;
    }
}