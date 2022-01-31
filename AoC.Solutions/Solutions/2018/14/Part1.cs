using System.Text;
using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = Play(int.Parse(Input[0]));

        return result;
    }

    private static string Play(int quitAfter)
    {
        var recipes = new LinkedList<byte>();

        recipes.AddFirst(3);

        recipes.AddLast(7);

        var elf1 = recipes.First;

        var elf2 = recipes.Last;

        while (true)
        {
            // ReSharper disable PossibleNullReferenceException
            var newRecipes = elf1.Value + elf2.Value;
            // ReSharper restore PossibleNullReferenceException

            if (newRecipes > 9)
            {
                recipes.AddLast((byte) (newRecipes / 10));
            }

            recipes.AddLast((byte) (newRecipes % 10));

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

            if (recipes.Count - 11 > quitAfter)
            {
                break;
            }
        }

        var node = recipes.First;

        for (var i = 0; i < quitAfter; i++)
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
}