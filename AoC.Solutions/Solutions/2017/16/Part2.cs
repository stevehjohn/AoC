using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var positions = Enumerable.Range(0, 16).ToArray();

        var names = Enumerable.Range(0, 16).ToArray();

        foreach (var step in Input[0].Split(','))
        {
            switch (step[0])
            {
                case 's':
                {
                    var length = int.FastParse(step[1..]) % 16;

                    var temp = new int[16];

                    for (var i = 0; i < 16; i++)
                    {
                        temp[(i + length) % 16] = positions[i];
                    }

                    positions = temp;

                    break;
                }
                case 'x':
                {
                    var indexes = step[1..].Split('/');

                    var a = int.FastParse(indexes[0]);
                    var b = int.FastParse(indexes[1]);

                    (positions[a], positions[b]) =
                        (positions[b], positions[a]);

                    break;
                }
                case 'p':
                {
                    var a = Array.IndexOf(names, step[1] - 'a');
                    var b = Array.IndexOf(names, step[3] - 'a');

                    (names[a], names[b]) = (names[b], names[a]);

                    break;
                }
            }
        }

        positions = Power(positions, 1_000_000_000);

        names = Power(names, 1_000_000_000);

        var dancers = new char[16];

        for (var i = 0; i < 16; i++)
        {
            dancers[i] = (char) ('a' + names[positions[i]]);
        }

        return new string(dancers);
    }

    private static int[] Power(int[] permutation, int exponent)
    {
        var result = Enumerable.Range(0, permutation.Length).ToArray();

        while (exponent > 0)
        {
            if ((exponent & 1) != 0)
            {
                result = Compose(result, permutation);
            }

            permutation = Compose(permutation, permutation);

            exponent >>= 1;
        }

        return result;
    }

    private static int[] Compose(int[] a, int[] b)
    {
        var result = new int[a.Length];

        for (var i = 0; i < result.Length; i++)
        {
            result[i] = a[b[i]];
        }

        return result;
    }
}