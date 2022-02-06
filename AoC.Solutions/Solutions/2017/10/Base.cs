using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._10;

public abstract class Base : Solution
{
    public override string Description => "Knot hash";

    public int[] RunRound(int[] data, int[] lengths, ref int position, ref int skipLength)
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