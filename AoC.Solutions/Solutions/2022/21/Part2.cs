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

        while (monkey.Name != HumanMonkeyName)
        {
            Console.Write($"{monkey.Name}: ");

            var leftMonkey = Monkeys[monkey.Left];

            var rightMonkey = Monkeys[monkey.Right];

            double left;

            double right;

            if (double.IsNaN(leftMonkey.Value))
            {
                left = expected;

                right = rightMonkey.Value;
            }
            else
            {
                left = leftMonkey.Value;

                right = expected;
            }

            switch (monkey.Operator)
            {
                case "-":
                    expected = right + left;
                    break;

                case "/":
                    expected = right * left;
                    break;

                case "*":
                    expected = right / left;
                    break;

                default:
                    expected = right - left;
                    break;
            }

            if (double.IsNaN(leftMonkey.Value))
            {
                Console.Write($"({expected}) {monkey.Operator} {right}");

                monkey = Monkeys[monkey.Left];
            }
            else
            {
                Console.Write($"{left} {monkey.Operator} ({expected})");
                
                monkey = Monkeys[monkey.Right];
            }

            Console.WriteLine();
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