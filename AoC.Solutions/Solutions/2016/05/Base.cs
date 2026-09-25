using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._05;

public abstract partial class Base : Solution
{
    public override string Description => "Password hash";

    protected IEnumerable<HashMatch> GetMatches()
    {
        var prefixBytes = Encoding.ASCII.GetBytes(Input[0]);

        var suffix = 1;

        const int batchSize = 4_096;

        var matches = new HashMatch?[batchSize];

        while (true)
        {
            var start = suffix;

            Parallel.For(0, batchSize, i =>
            {
                matches[i] = Calculate(prefixBytes, start + i);
            });

            for (var i = 0; i < batchSize; i++)
            {
                if (matches[i].HasValue)
                {
                    yield return matches[i]!.Value;
                }
            }

            suffix += batchSize;
        }
    }

    private static HashMatch? Calculate(byte[] prefixBytes, int suffix)
    {
        Span<byte> bytes = stackalloc byte[prefixBytes.Length + 10];

        prefixBytes.CopyTo(bytes);

        Utf8Formatter.TryFormat(suffix, bytes[prefixBytes.Length..], out var written);

        Span<byte> hash = stackalloc byte[16];

        MD5.TryHashData(bytes[..(prefixBytes.Length + written)], hash, out _);

        if (hash[0] != 0 || hash[1] != 0 || (hash[2] & 0b1111_0000) != 0)
        {
            return null;
        }

        return new HashMatch(suffix, hash[2] & 0b0000_1111, hash[3] >> 4);
    }

    protected static char ToHex(int value)
    {
        return value < 10 ? (char) ('0' + value) : (char) ('a' + value - 10);
    }

    // ReSharper disable once NotAccessedPositionalProperty.Global
}