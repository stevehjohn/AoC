using System.Text;
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._05;

public abstract partial class Base : Solution
{
    public override string Description => "Password hash";

    protected IEnumerable<HashMatch> GetMatches()
    {
        var prefixBytes = Encoding.ASCII.GetBytes(Input[0]);

        var suffix = 1;

        const int batchSize = 65_536;

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

    protected static char ToHex(int value)
    {
        return value < 10 ? (char) ('0' + value) : (char) ('a' + value - 10);
    }
}