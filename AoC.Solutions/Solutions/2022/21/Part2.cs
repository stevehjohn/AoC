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
            Console.Write($"{monkey.Name}: ");

            var leftMonkey = Monkeys[monkey.Left];

            var rightMonkey = Monkeys[monkey.Right];

            long value;

            // ReSharper disable PossibleInvalidOperationException
            if (leftMonkey.Value == null)
            {
                value = rightMonkey.Value.Value;
            }
            else
            {
                value = leftMonkey.Value.Value;
            }
            // ReSharper restore PossibleInvalidOperationException

            var swap = leftMonkey.Value == null;

            expected = (monkey.Operator, swap) switch
            {
                ("+", _) => expected - value,
                ("*", _) => expected / value,
                ("-", _) => expected + value,
                ("/", _) => expected * value,
                //("-", false) => expected - value,
                //("-", true) => value + expected,
                //("/", false) => expected / value,
                //("/", true) => value * expected,
                _ => throw new PuzzleException("Unknown operator.")
            };

            if (leftMonkey.Value == null)
            {
                Console.Write($"({expected}) {monkey.Operator} {value}");

                monkey = Monkeys[monkey.Left];
            }
            else
            {
                Console.Write($"{value} {monkey.Operator} ({expected})");

                monkey = Monkeys[monkey.Right];
            }

            Console.WriteLine();
        }

        return expected;
    }
}