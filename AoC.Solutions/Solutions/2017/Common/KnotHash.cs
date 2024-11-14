using System.Text;
using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2017.Common;

public static class KnotHash
{
    public static string MakeHash(string input)
    {
        var lengthsList = input.Select(c => (int) c).ToList();

        lengthsList.AddRange([17, 31, 73, 47, 23]);

        var lengths = lengthsList.ToArray();

        var skipLength = 0;

        var position = 0;

        var data = new int[256];

        for (var i = 0; i < 256; i++)
        {
            data[i] = i;
        }

        for (var i = 0; i < 64; i++)
        {
            data = RunRound(data, lengths, ref position, ref skipLength);
        }

        if (data == null)
        {
            throw new PuzzleException("This shouldn't happen.");
        }

        var hash = new int[16];

        for (var i = 0; i < 16; i++)
        {
            for (var x = 0; x < 16; x++)
            {
                hash[i] ^= data[i * 16 + x];
            }
        }

        var builder = new StringBuilder();

        foreach (var item in hash)
        {
            builder.Append(item.ToString("X2"));
        }

        var result = builder.ToString().ToLower();

        return result;
    }

    public static int[] RunRound(int[] data, int[] lengths, ref int position, ref int skipLength)
    {
        foreach (var length in lengths)
        {
            SwapBetween(data, position, length);

            position = (position + length + skipLength) % 256;

            skipLength++;
        }

        return data;
    }

    private static void SwapBetween(int[] data, int start, int length)
    {
        var end = (start + length - 1) % 256;

        for (var i = 0; i < length / 2; i++)
        {
            (data[start], data[end]) = (data[end], data[start]);

            start = (start + 1) % 256;

            end--;

            if (end < 0)
            {
                end = 255;
            }
        }
    }
}