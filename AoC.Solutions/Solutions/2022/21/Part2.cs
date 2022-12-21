// ReSharper disable SpecifyACultureInStringConversionExplicitly

namespace AoC.Solutions.Solutions._2022._21;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        Monkeys[HumanMonkeyName].Value = double.NaN;

        var rootMonkey = Monkeys[RootMonkeyName];

        var rootLeft = Solve(rootMonkey.Left);

        var rootRight = Solve(rootMonkey.Right);

        double answer;

        if (double.IsNaN(rootLeft))
        {
            answer = SolveForHuman(rootMonkey.Left, rootRight);
        }
        else
        {
            answer = SolveForHuman(rootMonkey.Right, rootLeft);
        }

        return answer.ToString();
    }

    private double SolveForHuman(string name, double expected)
    {
        var monkey = Monkeys[name];

        while (monkey.Name != RootMonkeyName)
        {
            Monkey valueMonkey;

            Monkey unknownMonkey;

            double leftValue;

            double rightValue;

            if (double.IsNaN(Monkeys[monkey.Left].Value))
            {
                valueMonkey = Monkeys[monkey.Right];

                unknownMonkey = Monkeys[monkey.Left];

                leftValue = expected;

                rightValue = valueMonkey.Value;
            }
            else
            {
                valueMonkey = Monkeys[monkey.Left];

                unknownMonkey = Monkeys[monkey.Right];

                leftValue = valueMonkey.Value;

                rightValue = expected;
            }

            var leftMonkey = Monkeys[monkey.Left];

            var rightMonkey = Monkeys[monkey.Right];

            //var leftValue = double.IsNaN(leftMonkey.Value) ? expected : leftMonkey.Value;

            //var rightValue = double.IsNaN(rightMonkey.Value) ? expected : rightMonkey.Value;

            switch (monkey.Operator)
            {
                case "-":
                    expected = leftValue + rightValue;
                    break;

                case "/":
                    expected = leftValue * rightValue;
                    break;

                case "*":
                    expected = leftValue / rightValue;
                    break;

                default:
                    expected = leftValue - rightValue;
                    break;
            }

            var left = leftMonkey.Value != 0 ? leftMonkey.Value.ToString() : monkey.Left;

            var right = rightMonkey.Value != 0 ? rightMonkey.Value.ToString() : monkey.Right;

            Console.WriteLine($"{monkey.Name}: {left} {monkey.Operator} {right} ({expected})");

            monkey = unknownMonkey;
        }

        return 0;
    }

    private double SolveForHumanX(string branchStart, double expected)
    {
        var monkey = Monkeys[HumanMonkeyName];

        while (monkey.Name != RootMonkeyName)
        {
            monkey = Monkeys.SingleOrDefault(m => m.Value.Left == monkey.Name).Value ?? Monkeys.SingleOrDefault(m => m.Value.Right == monkey.Name).Value;

            var left = Monkeys[monkey.Left].Value != 0 ? Monkeys[monkey.Left].Value.ToString() : monkey.Left;

            var right = Monkeys[monkey.Right].Value != 0 ? Monkeys[monkey.Right].Value.ToString() : monkey.Right;

            Console.WriteLine($"{monkey.Name}: {left} {monkey.Operator} {right}");
        }

        return 0d;
    }
}