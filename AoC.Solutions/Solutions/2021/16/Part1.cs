using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;
using System.Globalization;
using System.Text;

namespace AoC.Solutions.Solutions._2021._16;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string Description => "Packet processing";

    public override string GetAnswer()
    {
        var binary = new StringBuilder();

        var data = Input[0];

        for (var i = 0; i < data.Length; i ++)
        {
            var number = int.Parse(data.Substring(i, 1), NumberStyles.HexNumber);

            binary.Append(Convert.ToString(number, 2).PadLeft(4, '0'));
        }

        var binaryString = binary.ToString();

        var rootPacket = Packet.GetPackets(binaryString).Single();

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