using System.Numerics;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._15;

[UsedImplicitly]
public class Part2 : Base
{
    private const int Count = 5_000_000;

    private const long Modulus = int.MaxValue;

    private const int ChunkSize = 1_000_000;

    public override string GetAnswer()
    {
        var seedA = uint.Parse(Input[0].AsSpan(
            Input[0].LastIndexOf(' ') + 1));

        var seedB = uint.Parse(Input[1].AsSpan(
            Input[1].LastIndexOf(' ') + 1));

        var a = Generate(seedA, 16807, 3);

        var b = Generate(seedB, 48271, 7);

        var matches = 0;

        for (var i = 0; i < Count; i++)
        {
            if (a[i] == b[i])
            {
                matches++;
            }
        }

        return matches.ToString();
    }

    private static ushort[] Generate(uint seed, uint factor, uint mask)
    {
        var result = new ushort[Count];

        var written = 0;

        var start = 0L;

        var workers = Math.Clamp(Environment.ProcessorCount, 1, 8);

        while (written < Count)
        {
            var remaining = Count - written;

            var estimated = remaining * (mask + 1);

            var chunks = (int) Math.Clamp((estimated + ChunkSize - 1) / ChunkSize, 1L, workers);

            var results = new ushort[chunks][];

            var startCaptured = start;
            
            Parallel.For(0, chunks, chunk =>
            {
                var offset = startCaptured + (long) chunk * ChunkSize;

                var value = (uint) (seed * (long) BigInteger.ModPow(factor, offset, Modulus) % Modulus);

                var accepted = new List<ushort>(ChunkSize / (int) (mask + 1) + 1024);

                for (var i = 0; i < ChunkSize; i++)
                {
                    var product = (ulong) value * factor;

                    var reduced = (product & Modulus) + (product >> 31);

                    value = (uint) (reduced >= Modulus
                        ? reduced - Modulus
                        : reduced);

                    if ((value & mask) == 0)
                    {
                        accepted.Add((ushort) value);
                    }
                }

                results[chunk] = [.. accepted];
            });

            foreach (var chunk in results)
            {
                var length = Math.Min(chunk.Length, Count - written);

                Array.Copy(chunk, 0, result, written, length);

                written += length;

                if (written == Count)
                {
                    break;
                }
            }

            start += (long) chunks * ChunkSize;
        }

        return result;
    }
}