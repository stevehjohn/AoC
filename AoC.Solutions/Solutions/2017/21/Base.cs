using System.Numerics;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._21;

public abstract class Base : Solution
{
    public override string Description => "Fractal art";

    protected int GetAnswer(int cycles)
    {
        var rules2 = new int[16];

        var rules3 = new int[512];

        foreach (var line in Input)
        {
            var parts = line.Split(" => ");

            var size = parts[0].Length == 5 ? 2 : 3;

            var pattern = Encode(parts[0]);

            var result = Encode(parts[1]);

            var rules = size == 2 ? rules2 : rules3;

            for (var rotation = 0; rotation < 4; rotation++)
            {
                rules[pattern] = result;

                rules[Flip(pattern, size)] = result;

                pattern = Rotate(pattern, size);
            }
        }

        var counts = new int[512];

        var next = new int[512];

        var transitions = new int[512][];

        counts[Encode(".#./..#/###")] = 1;

        while (cycles >= 3)
        {
            Array.Clear(next);

            for (var pattern = 0; pattern < 512; pattern++)
            {
                var count = counts[pattern];

                if (count == 0)
                {
                    continue;
                }

                var tiles = transitions[pattern] ??= ExpandThree(pattern, rules2, rules3);

                foreach (var tile in tiles)
                {
                    next[tile] += count;
                }
            }

            (counts, next) = (next, counts);

            cycles -= 3;
        }

        var total = 0;

        for (var pattern = 0; pattern < 512; pattern++)
        {
            if (counts[pattern] == 0)
            {
                continue;
            }

            var lit = cycles switch
            {
                0 => BitOperations.PopCount((uint) pattern),
                1 => BitOperations.PopCount((uint) rules3[pattern]),
                _ => CountAfterTwo(rules3[pattern], rules2)
            };

            total += lit * counts[pattern];
        }

        return total;
    }

    private static int[] ExpandThree(
        int pattern, int[] rules2, int[] rules3)
    {
        var four = rules3[pattern];

        ulong six = 0;

        for (var y = 0; y < 2; y++)
        {
            for (var x = 0; x < 2; x++)
            {
                var tile = Extract((ulong) four, 4, x * 2, y * 2, 2);

                Insert(ref six, 6, x * 3, y * 3, 3, rules2[tile]);
            }
        }

        var result = new int[9];

        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                var tile = Extract(six,
                    6, x * 2, y * 2, 2);

                result[y * 3 + x] = rules2[tile];
            }
        }

        return result;
    }

    private static int CountAfterTwo(int four, int[] rules2)
    {
        var count = 0;

        for (var y = 0; y < 4; y += 2)
        {
            for (var x = 0; x < 4; x += 2)
            {
                var tile = Extract((ulong) four, 4, x, y, 2);

                count += BitOperations.PopCount((uint) rules2[tile]);
            }
        }

        return count;
    }

    private static int Extract(
        ulong grid, int width, int x, int y, int size)
    {
        var result = 0;

        var mask = (1UL << size) - 1;

        for (var row = 0; row < size; row++)
        {
            result |= (int) ((grid >> ((y + row) * width + x)) & mask) << (row * size);
        }

        return result;
    }

    private static void Insert(
        ref ulong grid, int width,
        int x, int y, int size, int tile)
    {
        var mask = (1 << size) - 1;

        for (var row = 0; row < size; row++)
        {
            grid |= (ulong) ((tile >> (row * size)) & mask) << ((y + row) * width + x);
        }
    }

    private static int Encode(string pattern)
    {
        var result = 0;

        var bit = 0;

        foreach (var c in pattern)
        {
            if (c == '/')
            {
                continue;
            }

            if (c == '#')
            {
                result |= 1 << bit;
            }

            bit++;
        }

        return result;
    }

    private static int Rotate(int pattern, int size)
    {
        var result = 0;

        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                var bit = (pattern >> (y * size + x)) & 1;

                result |= bit << (x * size + size - 1 - y);
            }
        }

        return result;
    }

    private static int Flip(int pattern, int size)
    {
        var result = 0;

        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                var bit = (pattern >> (y * size + x)) & 1;

                result |= bit << (y * size + size - 1 - x);
            }
        }

        return result;
    }
}