using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._15;

public abstract class Base : Solution
{
    public override string Description => "Number memory game";

    protected int GetAnswer(int turns)
    {
        var numbers = Input[0].Split(',').Select(int.FastParse).ToArray();

        if (turns <= numbers.Length)
        {
            return numbers[turns - 1];
        }

        var lastSpoken = new int[Math.Max(turns, numbers.Max() + 1)];

        for (var i = 0; i < numbers.Length - 1; i++)
        {
            lastSpoken[numbers[i]] = i + 1;
        }

        var last = numbers[^1];

        for (var turn = numbers.Length; turn < turns; turn++)
        {
            var previous = lastSpoken[last];

            lastSpoken[last] = turn;

            last = previous == 0 ? 0 : turn - previous;
        }

        return last;
    }
}