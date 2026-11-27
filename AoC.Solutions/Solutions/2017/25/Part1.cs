using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._25;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var state = (Input[0][^2] - 'A') * 2;

        var stepsLine = Input[1];

        var start = stepsLine.IndexOf(
            "after ", StringComparison.Ordinal) + 6;

        var end = stepsLine.IndexOf(' ', start);

        var steps = int.FastParse(
            stepsLine.AsSpan(start, end - start));

        var writes = new byte[52];

        var directions = new int[52];

        var nextStates = new int[52];

        for (var i = 3; i < Input.Length; i += 10)
        {
            var index = (Input[i][^2] - 'A') * 2;

            writes[index] = (byte) (Input[i + 2][^2] - '0');

            directions[index] = Input[i + 3][^3] == 'h'
                ? 1 : -1;

            nextStates[index] = (Input[i + 4][^2] - 'A') * 2;

            writes[index + 1] = (byte) (Input[i + 6][^2] - '0');

            directions[index + 1] = Input[i + 7][^3] == 'h'
                ? 1 : -1;

            nextStates[index + 1] = (Input[i + 8][^2] - 'A') * 2;
        }

        var tape = new byte[steps * 2 + 1];

        var position = steps;

        var checksum = 0;

        for (var i = 0; i < steps; i++)
        {
            var value = tape[position];

            var transition = state | value;

            var write = writes[transition];

            checksum += write - value;

            tape[position] = write;

            position += directions[transition];

            state = nextStates[transition];
        }

        return checksum.ToString();
    }
}