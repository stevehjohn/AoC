using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._16;

public abstract class Base : Solution
{
    public override string Description => "Dragon checksum";

    protected static string GetChecksum(string data)
    {
        var checksum = data;

        while (checksum.Length % 2 == 0)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < checksum.Length; i += 2)
            {
                builder.Append(checksum[i] == checksum[i + 1] ? '1' : '0');
            }

            checksum = builder.ToString();
        }

        return checksum;
    }

    protected static string CalculateChecksum(string seed, int length)
    {
        var blockSize = length & -length;

        var lengths = new List<int> { seed.Length };

        var parities = new List<int>();

        var seedParity = new int[seed.Length + 1];

        for (var i = 0; i < seed.Length; i++)
        {
            seedParity[i + 1] = seedParity[i] ^ (seed[i] - '0');
        }

        parities.Add(seedParity[^1]);

        while (lengths[^1] < length)
        {
            var previousLength = lengths[^1];

            lengths.Add(previousLength * 2 + 1);

            parities.Add(previousLength & 1);
        }

        var result = new char[length / blockSize];

        var previous = 0;

        for (var i = 0; i < result.Length; i++)
        {
            var end = (i + 1) * blockSize;

            var current = PrefixParity(end, lengths, parities, seedParity);

            var parity = current ^ previous;

            result[i] = (char) ('0' + (blockSize == 1 ? parity : parity ^ 1));

            previous = current;
        }

        return new string(result);
    }

    private static int PrefixParity(int count, List<int> lengths, List<int> parities, int[] seedParity)
    {
        var level = lengths.Count - 1;

        var parity = 0;

        while (level > 0)
        {
            var previousLength = lengths[level - 1];

            if (count <= previousLength)
            {
                level--;

                continue;
            }

            if (count == previousLength + 1)
            {
                return parity ^ parities[level - 1];
            }

            var tail = count - previousLength - 1;

            parity ^= tail & 1;

            count = previousLength - tail;

            level--;
        }

        return parity ^ seedParity[count];
    }
}