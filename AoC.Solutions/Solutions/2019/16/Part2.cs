using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var input = Input[0];

        var inputLength = input.Length;

        var totalLength = inputLength * 10_000;

        var offset = int.FastParse(input.AsSpan(0, 7));

        if (offset < totalLength / 2)
        {
            throw new InvalidOperationException("Offset must be in the second half.");
        }

        var length = totalLength - offset;

        const int period = 16_000;

        var coefficients = new int[period];

        for (var k = 0; k < period; k++)
        {
            coefficients[k] = Coefficient(k);
        }

        var weights = new long[inputLength];

        var position = 0;

        var coefficient = 0;

        var commonLength = length - 7;

        for (var k = 0; k < commonLength; k++)
        {
            weights[position] += coefficients[coefficient];

            if (++position == inputLength)
            {
                position = 0;
            }

            if (++coefficient == period)
            {
                coefficient = 0;
            }
        }

        Span<char> result = stackalloc char[8];

        for (var i = 0; i < 8; i++)
        {
            var sum = 0L;

            position = (offset + i) % inputLength;

            for (var j = 0; j < inputLength; j++)
            {
                sum += weights[j] * (input[position] - '0');

                if (++position == inputLength)
                {
                    position = 0;
                }
            }

            for (var k = commonLength; k < length - i; k++)
            {
                sum += coefficients[k % period] * (input[(offset + i + k) % inputLength] - '0');
            }

            result[i] = (char) ('0' + sum % 10);
        }

        return new string(result);
    }

    private static int Coefficient(int k)
    {
        var n = k + 99;

        var mod2 = (n & 99) == 99 ? 1 : 0;

        // 99 in base 5 is 344.
        var mod5 = 0;

        if (n % 5 == 4 && n / 5 % 5 == 4)
        {
            mod5 = (n / 25 % 5) switch
            {
                3 => 1,
                4 => 4,
                _ => 0
            };
        }

        return (mod5 & 1) == mod2 ? mod5 : mod5 + 5;
    }
}