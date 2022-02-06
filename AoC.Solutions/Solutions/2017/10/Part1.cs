using System.Runtime.ExceptionServices;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var circle = new CircularLinkedList<int>();

        for (var i = 0; i < 5; i++)
        {
            circle.Add(i);
        }

        var current = circle.First;

        var lengths = "3, 4, 1, 5".Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

        var skipLength = 0;

        foreach (var length in lengths)
        {
            var sectionEnd = current.Skip(length - 1);

            circle.Swap(current, sectionEnd);

            current = current.Next.Skip(skipLength);

            skipLength++;

            // DUMP
            var node = circle.First;

            do
            {
                Console.Write($"{node.Value} ");

                node = node.Next;

            } while (node != circle.First);

            Console.WriteLine();
        }

        var result = circle.First.Value * circle.First.Next.Value;

        return result.ToString();
    }
}