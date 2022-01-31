using System.Text;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._14;

public abstract class Base : Solution
{
    public override string Description => "Elf recipes";
    
    protected static string Play(int input, bool isPart2 = false)
    {
        var recipes = new LinkedList<byte>();

        recipes.AddFirst(3);

        recipes.AddLast(7);

        var elf1 = recipes.First;

        var elf2 = recipes.Last;

        var searchString = input.ToString().Reverse().Select(c => (byte) (c - '0')).ToArray();

        while (true)
        {
            // ReSharper disable PossibleNullReferenceException
            var newRecipes = elf1.Value + elf2.Value;
            // ReSharper restore PossibleNullReferenceException

            if (newRecipes > 9)
            {
                recipes.AddLast((byte) (newRecipes / 10));

                if (isPart2)
                {
                    var match = Match(recipes, searchString);

                    if (match != null)
                    {
                        return match;
                    }
                }
            }

            recipes.AddLast((byte) (newRecipes % 10));

            if (isPart2)
            {
                var match = Match(recipes, searchString);

                if (match != null)
                {
                    return match;
                }
            }

            var moves = elf1.Value + 1;

            for (var i = 0; i < moves; i++)
            {
                elf1 = elf1.NextCircular();
            }

            moves = elf2.Value + 1;

            for (var i = 0; i < moves; i++)
            {
                elf2 = elf2.NextCircular();
            }

            if (! isPart2 && recipes.Count - 11 > input)
            {
                break;
            }
        }

        var node = recipes.First;

        for (var i = 0; i < input; i++)
        {
            // ReSharper disable once PossibleNullReferenceException
            node = node.Next;
        }

        var builder = new StringBuilder();

        for (var i = 0; i < 10; i++)
        {
            // ReSharper disable once PossibleNullReferenceException
            builder.Append(node.Value);

            node = node.Next;
        }

        return builder.ToString();
    }

    private static string Match(LinkedList<byte> recipes, byte[] searchString)
    {
        var i = 0;

        var checker = recipes.Last;

        if (recipes.Count > searchString.Length)
        {
            var matched = true;

            while (recipes.Count > searchString.Length && i < searchString.Length)
            {
                // ReSharper disable once PossibleNullReferenceException
                if (checker.Value != searchString[i])
                {
                    matched = false;

                    break;
                }

                checker = checker.Previous;

                i++;
            }

            if (matched)
            {
                i = 0;

                while (checker != null)
                {
                    i++;

                    checker = checker.Previous;
                }

                return i.ToString();
            }
        }

        return null;
    }
}