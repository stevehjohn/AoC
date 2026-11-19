using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2016.Common;

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

            Parallel.For(0, batchSize / 4, group =>
            {
                var offset = group * 4;

                Span<byte> hashes = stackalloc byte[64];

                Md5Four.Hash(prefixBytes, start + offset, hashes);

                for (var lane = 0; lane < 4; lane++)
                {
                    var hash = hashes.Slice(lane * 16, 16);

                    matches[offset + lane] = hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0
                            ? new HashMatch(start + offset + lane, hash[2] & 0x0F, hash[3] >> 4)
                            : null;
                }
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
        // ReSharper disable once IteratorNeverReturns
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
}