using AoC.Infrastructure;

namespace AoC.Solutions._2021._10;

public abstract class Base : Solution
{
    protected static readonly Dictionary<char, char> Pairs = new()
                                                             {
                                                                 { '(', ')' },
                                                                 { '[', ']' },
                                                                 { '{', '}' },
                                                                 { '<', '>' }
                                                             };

    protected static char IsCorrupt(string input)
    {
        var stack = new Stack<char>();

        foreach (var c in input)
        {
            if (Pairs.Keys.Contains(c))
            {
                stack.Push(c);

                continue;
            }

            if (Pairs[stack.Pop()] != c)
            {
                return c;
            }
        }

        return '\0';
    }
}