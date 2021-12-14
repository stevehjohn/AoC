using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._14;

public abstract class Base : Solution
{
    public string GetAnswer(int steps)
    {
        var polymer = new LinkedList<char>(Input[0].ToCharArray());

        for (var i = 0; i < steps; i++)
        {
            Console.WriteLine(i);

            LinkedListNode<char> node;

            var charactersToAdd = new Dictionary<int, char>();

            int position;

            for (var r = 2; r < Input.Length; r++)
            {
                var rule = Input[r].Split("->", StringSplitOptions.TrimEntries);

                node = polymer.First;

                position = 1;

                while (node?.Next != null)
                {
                    if (node.Value == rule[0][0] && node.Next.Value == rule[0][1])
                    {
                        charactersToAdd.Add(position, rule[1][0]);
                    }

                    position++;

                    node = node.Next;
                }
            }

            node = polymer.First;

            position = 1;

            while (node?.Next != null)
            {
                if (! charactersToAdd.ContainsKey(position))
                {
                    position++;

                    node = node.Next;

                    continue;
                }

                node = polymer.AddAfter(node, charactersToAdd[position]).Next;

                position++;
            }
        }

        var counts = new int[26];

        foreach (var item in polymer)
        {
            counts[item - 'A']++;
        }

        return (counts.Max() - counts.Where(c => c > 0).Min()).ToString();
    }
}