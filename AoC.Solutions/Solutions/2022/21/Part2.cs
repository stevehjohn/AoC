﻿// ReSharper disable SpecifyACultureInStringConversionExplicitly

using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._21;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        Monkeys[HumanMonkeyName].Value = null;

        var rootMonkey = Monkeys[RootMonkeyName];

        var rootLeft = Solve(rootMonkey.Left);

        var rootRight = Solve(rootMonkey.Right);

        double answer;

        // ReSharper disable PossibleInvalidOperationException
        if (rootLeft == null)
        {
            answer = SolveForHuman(rootMonkey.Left, rootRight.Value);
        }
        else
        {
            answer = SolveForHuman(rootMonkey.Right, rootLeft.Value);
        }
        // ReSharper restore PossibleInvalidOperationException

        Console.WriteLine();

        return answer.ToString();
    }

    private long SolveForHuman(string name, long expected)
    {
        var monkey = Monkeys[name];

        while (monkey.Name != HumanMonkeyName)
        {
            var leftMonkey = Monkeys[monkey.Left];

            var rightMonkey = Monkeys[monkey.Right];

            var value = leftMonkey.Value ?? rightMonkey.Value!.Value;

            var swap = leftMonkey.Value == null;

            expected = (monkey.Operator, swap) switch
            {
                ("+", _) => expected - value,
                ("*", _) => expected / value,
                ("-", false) => value - expected,
                ("-", true) => expected + value,
                ("/", false) => value / expected,
                ("/", true) => expected * value,
                _ => throw new PuzzleException("Unknown operator.")
            };

            monkey = leftMonkey.Value == null
                         ? Monkeys[monkey.Left]
                         : Monkeys[monkey.Right];
        }

        return expected;
    }
}