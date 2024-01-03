using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        ExpandInput();

        for (var i = 0; i < 10_000_000; i++)
        {
            PerformMove();
        }

        var first = Cups[1];

        var second = Cups[first];

        var result = (long) first * second;

        return result.ToString();
    }

    private void ExpandInput()
    {
        var input = new int[1_000_001];

        Buffer.BlockCopy(Cups, 0, input, 0, Cups.Length * sizeof(int));

        for (var i = Cups.Length; i < 1_000_001; i++)
        {
            input[i] = i + 1;
        }

        input[Input[0][Input[0].Length - 1] - '0'] = Cups.Length;

        input[1_000_000] = Input[0][0] - '0';

        Cups = input;

        Max = 1_000_000;
    }
}