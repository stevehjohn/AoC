using System.Runtime.ExceptionServices;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var circle = new CircularLinkedList<int>();

        for (var i = 0; i < 256; i++)
        {
            circle.Add(i);
        }

        var current = circle.First;

        var lengths = Input[0].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

        var skipLength = 0;

        foreach (var length in lengths)
        {
            var sectionEnd = current.Skip(length - 1);

            // DUMP
            var node = circle.First;

            do
            {
                Console.ForegroundColor = node == current ? ConsoleColor.Blue : node == sectionEnd ? ConsoleColor.Red : ConsoleColor.Green;

                Console.Write($"{node.Value, 5} ");

                node = node.Next;

            } while (node != circle.First);

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\n");

            circle.Swap(current, sectionEnd);

            current = current.Next.Skip(skipLength);

            skipLength++;
        }

        var result = circle.First.Value * circle.First.Next.Value;

        return result.ToString();
    }
}