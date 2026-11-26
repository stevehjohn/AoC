using System.Text;
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

        var digits = input.ToString().Length;

        var modulus = 1L;

        for (var i = 0; i < digits; i++)
        {
            modulus *= 10;
        }

        var window = 37L % modulus;

        if (isPart2)
        {
            if (digits == 1 && input == 3)
            {
                return "0";
            }

            if (count >= digits && window == input)
            {
                return (count - digits).ToString();
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

                if (isPart2)
                {
                    window = (window * 10 + 1) % modulus;

                    if (count >= digits && window == input)
                    {
                        return (count - digits).ToString();
                    }
                }

                sum -= 10;
            }

            recipes[count++] = (byte) sum;

            if (isPart2)
            {
                window = (window * 10 + sum) % modulus;

                if (count >= digits && window == input)
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

        var builder = new StringBuilder(10);

        for (var i = input; i < input + 10; i++)
        {
            builder.Append((char) ('0' + recipes[i]));
        }

        return builder.ToString();
    }
}