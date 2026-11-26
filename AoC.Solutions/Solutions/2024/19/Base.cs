using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._19;

public abstract class Base : Solution
{
    public override string Description => "Linen layout";

    protected bool IsPart2;

    private const int AlphabetSize = 5;

    private int[] _trie;

    private bool[] _terminal;

    protected void ParseInput()
    {
        var towels = Input[0].Split(
            ',', StringSplitOptions.TrimEntries);

        var capacity = towels.Sum(t => t.Length) + 1;

        _trie = new int[capacity * AlphabetSize];

        _terminal = new bool[capacity];

        var nodes = 1;

        foreach (var towel in towels)
        {
            var node = 0;

            foreach (var colour in towel)
            {
                var index = node * AlphabetSize + Colour(colour);

                if (_trie[index] == 0)
                {
                    _trie[index] = nodes++;
                }

                node = _trie[index];
            }

            _terminal[node] = true;
        }
    }

    protected long CheckEachTowel()
    {
        var count = 0L;

        var maxLength = 0;

        for (var i = 2; i < Input.Length; i++)
        {
            maxLength = Math.Max(maxLength, Input[i].Length);
        }

        var ways = new long[maxLength + 1];

        for (var i = 2; i < Input.Length; i++)
        {
            var result = CountPossibilities(Input[i], ways);

            count += IsPart2 ? result : result > 0 ? 1 : 0;
        }

        return count;
    }

    private long CountPossibilities(string design, long[] ways)
    {
        ways[design.Length] = 1;

        for (var i = design.Length - 1; i >= 0; i--)
        {
            var node = 0;

            var count = 0L;

            for (var j = i; j < design.Length; j++)
            {
                node = _trie[node * AlphabetSize + Colour(design[j])];

                if (node == 0)
                {
                    break;
                }

                if (_terminal[node])
                {
                    count += ways[j + 1];

                    if (! IsPart2 && count > 0)
                    {
                        break;
                    }
                }
            }

            ways[i] = count;
        }

        return ways[0];
    }

    private static int Colour(char colour)
    {
        return colour switch
        {
            'w' => 0,
            'u' => 1,
            'b' => 2,
            'r' => 3,
            'g' => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(colour))
        };
    }
}