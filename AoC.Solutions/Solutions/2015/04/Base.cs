using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._04;

public abstract class Base : Solution
{
    public override string Description => "Crypto stocking stuffer";

    protected int GetAnswer(string pattern)
    {
        var key = Input[0];

        var keyBytes = Encoding.ASCII.GetBytes(key);

        var requireSixZeroes = pattern.Length == 6;

        const int batchSize = 16_384;

        var matches = new bool[batchSize];

        var i = 1;

        while (true)
        {
            var start = i;

            Parallel.For(0, batchSize, x =>
            {
                matches[x] = IsMatch(keyBytes, start + x, requireSixZeroes);
            });

            for (var x = 0; x < batchSize; x++)
            {
                if (matches[x])
                {
                    return start + x;
                }
            }

            i += batchSize;
        }
    }

    private static bool IsMatch(byte[] keyBytes, int value, bool requireSixZeroes)
    {
        Span<byte> buffer = stackalloc byte[keyBytes.Length + 11];

        keyBytes.CopyTo(buffer);

        Utf8Formatter.TryFormat(value, buffer[keyBytes.Length..], out var written);

        Span<byte> hash = stackalloc byte[16];

        MD5.TryHashData(buffer[..(keyBytes.Length + written)], hash, out _);

        return hash[0] == 0 && hash[1] == 0 && (requireSixZeroes ? hash[2] == 0 : (hash[2] & 0xf0) == 0);
    }
}