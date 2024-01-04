using System.Text;

namespace AoC.Solutions.Solutions._2021._16;

public class Packet
{
    public int Version { get; private init; }

    private int Type { get; init; }

    private int Length { get; set; }

    private long Value { get; set; }

    public List<Packet> SubPackets { get; }

    public Packet()
    {
        SubPackets = new List<Packet>();
    }

    public static List<Packet> GetPackets(string input, int maximum = 1)
    {
        var position = 0;

        var packets = new List<Packet>();

        while (position < input.Length && packets.Count < maximum)
        {
            var packet = Parse(input.Substring(position));

            position += packet.Length;

            packets.Add(packet);
        }

        return packets;
    }

    public long ProcessPacket()
    {
        switch (Type)
        {
            case 0:
                return SubPackets.Sum(p => p.ProcessPacket());
            case 1:
                var product = 1L;

                SubPackets.ForEach(p => product *= p.ProcessPacket());

                return product;
            case 2:
                return SubPackets.Min(p => p.ProcessPacket());
            case 3:
                return SubPackets.Max(p => p.ProcessPacket());
            case 5:
                return SubPackets[0].ProcessPacket() > SubPackets[1].ProcessPacket() ? 1 : 0;
            case 6:
                return SubPackets[0].ProcessPacket() < SubPackets[1].ProcessPacket() ? 1 : 0;
            case 7:
                return SubPackets[0].ProcessPacket() == SubPackets[1].ProcessPacket() ? 1 : 0;
        }

        return Value;
    }

    private static Packet Parse(string input)
    {
        var version = GetValue(input, 0, 3);

        var type = GetValue(input, 3, 3);

        var position = 6;

        var packet = new Packet
                     {
                         Version = version,
                         Type = type
                     };

        switch (type)
        {
            case 4:
                var binaryValue = new StringBuilder();

                while (input[position] == '1')
                {
                    binaryValue.Append(input.Substring(position + 1, 4));

                    position += 5;
                }

                binaryValue.Append(input.Substring(position + 1, 4));

                position += 5;

                packet.Value = Convert.ToInt64(binaryValue.ToString(), 2);

                break;
            default:
                var lengthType = input[position];

                position++;

                if (lengthType == '0')
                {
                    var length = GetValue(input, position, 15);

                    position += 15;

                    packet.SubPackets.AddRange(GetPackets(input.Substring(position, length), int.MaxValue));

                    position += length;
                }
                else
                {
                    var subPackets = GetValue(input, position, 11);

                    position += 11;

                    packet.SubPackets.AddRange(GetPackets(input.Substring(position), subPackets));

                    position += packet.SubPackets.Sum(p => p.Length);
                }

                break;
        }

        packet.Length = position;

        return packet;
    }

    private static int GetValue(string input, int start, int length)
    {
        var number = Convert.ToInt32(input.Substring(start, length), 2);

        return number;
    }
}