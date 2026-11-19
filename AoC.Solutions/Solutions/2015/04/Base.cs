using System.Text;
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._04;

public abstract class Base : Solution
{
    public override string Description => "Crypto stocking stuffer";

    protected int GetAnswer(string pattern)
    {
        var keyBytes = Encoding.ASCII.GetBytes(Input[0]);

        var requireSixZeroes = pattern.Length == 6;

        const int batchSize = 16_384;

        var matches = new bool[batchSize];

        var i = 1;

        while (true)
        {
            var start = i;

            Parallel.For(0, batchSize / 4, group =>
            {
                var offset = group * 4;

                Span<byte> hashes = stackalloc byte[64];

                Md5Four.Hash(keyBytes, start + offset, hashes);

                for (var lane = 0; lane < 4; lane++)
                {
                    var hash = hashes.Slice(lane * 16, 16);

                    matches[offset + lane] =
                        hash[0] == 0 &&
                        hash[1] == 0 &&
                        (requireSixZeroes
                            ? hash[2] == 0
                            : (hash[2] & 0xF0) == 0);
                }
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
}