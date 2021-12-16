namespace AoC.Solutions.Solutions._2021._16;

public class Packet
{
    public int Version { get; private init; }

    public int Length { get; private set; }

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

    private static Packet Parse(string input)
    {
        var version = GetValue(input, 0, 3);

        var packet = new Packet
                     {
                         Version = version
                     };

        var type = GetValue(input, 3, 3);

        var position = 6;

        switch (type)
        {
            case 4:
                while (input[position] == '1')
                {
                    position += 5;
                }

                position += 5;

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