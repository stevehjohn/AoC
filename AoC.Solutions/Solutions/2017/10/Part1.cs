using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var lengths = Input[0].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

        var data = new int[256];

        for (var i = 0; i < 256; i++)
        {
            data[i] = i;
        }

        var skipLength = 0;

        var position = 0;

        foreach (var length in lengths)
        {
            SwapBetween(data, position, length);

            position = (position + length + skipLength) % 256;

            skipLength++;
        }

        return (data[0] * data[1]).ToString();
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