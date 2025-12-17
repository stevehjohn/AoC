using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var length = Input[0].Length;

        var numbers = new int[Input.Length];

        for (var i = 0; i < Input.Length; i++)
        {
            numbers[i] = ParseBinary(Input[i]);
        }

        var bit = 1 << (length - 1);

        var value = 0;

        var half = Input.Length / 2;

        for (var i = 0; i < length; i++)
        {
            var ones = 0;

            for (var l = 0; l < Input.Length; l++)
            {
                if ((numbers[l] & bit) > 0)
                {
                    ones++;

                    if (ones > half)
                    {
                        value += bit;

                        break;
                    }
                }
            }

            bit >>= 1;
        }

        var mask = (1 << length) - 1;

        return (value * (value ^ mask)).ToString();
    }

    private static int ParseBinary(string line)
    {
        var value = 0;

        for (var i = 0; i < line.Length; i++)
        {
            value = (value << 1) | (line[i] == '1' ? 1 : 0);
        }

        return value;
    }
}