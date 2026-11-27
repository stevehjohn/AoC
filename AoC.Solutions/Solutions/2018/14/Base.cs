using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._14;

public abstract class Base : Solution
{
    public override string Description => "Elf recipes";

    protected static string Play(int input, bool isPart2 = false)
    {
        var recipes = new byte[Math.Max(input + 12, 1_024)];

        recipes[0] = 3;

        recipes[1] = 7;

        var count = 2;

        var elf1 = 0;

        var elf2 = 1;

        var pattern = input.ToString().Select(c => (byte) (c - '0')).ToArray();

        var digits = pattern.Length;

        var lastDigit = pattern[^1];

        if (isPart2)
        {
            for (var i = digits; i <= count; i++)
            {
                if (recipes.AsSpan(i - digits, digits).SequenceEqual(pattern))
                {
                    return (i - digits).ToString();
                }
            }
        }

        while (true)
        {
            var a = recipes[elf1];

            var b = recipes[elf2];

            var sum = a + b;

            if (count + 2 > recipes.Length)
            {
                Array.Resize(ref recipes, recipes.Length * 2);
            }

            if (sum >= 10)
            {
                recipes[count++] = 1;

                if (isPart2 && lastDigit == 1 && count >= digits && recipes.AsSpan(count - digits, digits).SequenceEqual(pattern))
                {
                    return (count - digits).ToString();
                }

                sum -= 10;
            }

            recipes[count++] = (byte) sum;

            if (isPart2)
            {
                if (sum == lastDigit && count >= digits && recipes.AsSpan(count - digits, digits).SequenceEqual(pattern))
                {
                    return (count - digits).ToString();
                }
            }
            else if (count >= input + 10)
            {
                break;
            }

            elf1 += a + 1;

            while (elf1 >= count)
            {
                elf1 -= count;
            }

            elf2 += b + 1;

            while (elf2 >= count)
            {
                elf2 -= count;
            }
        }

        return string.Create(10, (recipes, input),
            static (span, state) =>
            {
                for (var i = 0; i < span.Length; i++)
                {
                    span[i] = (char) ('0' +
                                      state.recipes[state.input + i]);
                }
            });
    }
}