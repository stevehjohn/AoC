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

        var search = input.ToString().Select(c => (byte) (c - '0')).ToArray();

        while (true)
        {
            var newRecipes = recipes[elf1] + recipes[elf2];

            if (newRecipes >= 10)
            {
                EnsureCapacity(ref recipes, count + 1);

                recipes[count++] = 1;

                if (isPart2 && Matches(recipes, count, search))
                {
                    return (count - search.Length).ToString();
                }
            }

            EnsureCapacity(ref recipes, count + 1);

            recipes[count++] = (byte) (newRecipes % 10);

            if (isPart2 && Matches(recipes, count, search))
            {
                return (count - search.Length).ToString();
            }

            elf1 = (elf1 + recipes[elf1] + 1) % count;
            elf2 = (elf2 + recipes[elf2] + 1) % count;

            if (! isPart2 && count >= input + 10)
            {
                break;
            }
        }

        var builder = new StringBuilder(10);

        for (var i = input; i < input + 10; i++)
        {
            builder.Append((char) ('0' + recipes[i]));
        }

        return builder.ToString();
    }

    private static bool Matches(byte[] recipes, int count, byte[] search)
    {
        if (count < search.Length)
        {
            return false;
        }

        var start = count - search.Length;

        for (var i = 0; i < search.Length; i++)
        {
            if (recipes[start + i] != search[i])
            {
                return false;
            }
        }

        return true;
    }

    private static void EnsureCapacity(ref byte[] recipes, int required)
    {
        if (required <= recipes.Length)
        {
            return;
        }

        Array.Resize(ref recipes, recipes.Length * 2);
    }
}