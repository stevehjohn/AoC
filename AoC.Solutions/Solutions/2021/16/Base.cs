using AoC.Solutions.Infrastructure;
using System.Globalization;
using System.Text;

namespace AoC.Solutions.Solutions._2021._16;

public abstract class Base : Solution
{
    public override string Description => "Packet processing";

    protected Packet GetRootPacket()
    {
        var binary = new StringBuilder();

        var data = Input[0];

        for (var i = 0; i < data.Length; i++)
        {
            var number = int.Parse(data.Substring(i, 1), NumberStyles.HexNumber);

            binary.Append(Convert.ToString(number, 2).PadLeft(4, '0'));
        }

        var binaryString = binary.ToString();

        return Packet.GetPackets(binaryString).Single();
    }
}