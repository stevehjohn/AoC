using System.Numerics;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var seedA = long.Parse(Input[0].Split(' ')[^1]);

        var seedB = long.Parse(Input[1].Split(' ')[^1]);

        const long modulus = int.MaxValue;

        const int iterations = 40_000_000;

        var workers = Environment.ProcessorCount;

        var matches = 0;

        Parallel.For(0, workers, worker =>
        {
            var start = (long) worker * iterations / workers;

            var end = (long) (worker + 1) * iterations / workers;

            var generatorA = seedA *
                             (long) BigInteger.ModPow(16807, start, modulus)
                             % modulus;

            var generatorB = seedB *
                             (long) BigInteger.ModPow(48271, start, modulus)
                             % modulus;

            var localMatches = 0;

            for (var i = start; i < end; i++)
            {
                var productA = generatorA * 16807;

                generatorA = (productA & modulus) +
                             (productA >> 31);

                if (generatorA >= modulus)
                {
                    generatorA -= modulus;
                }

                var productB = generatorB * 48271;

                generatorB = (productB & modulus) +
                             (productB >> 31);

                if (generatorB >= modulus)
                {
                    generatorB -= modulus;
                }

                if ((ushort) generatorA == (ushort) generatorB)
                {
                    localMatches++;
                }
            }

            Interlocked.Add(ref matches, localMatches);
        });

        return matches.ToString();
    }
}