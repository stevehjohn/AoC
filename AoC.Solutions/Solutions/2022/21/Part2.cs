// ReSharper disable SpecifyACultureInStringConversionExplicitly
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

            long left;

            long right;

            // ReSharper disable PossibleInvalidOperationException
            if (leftMonkey.Value == null)
            {
                left = expected;

                right = rightMonkey.Value.Value;
            }
            else
            {
                left = leftMonkey.Value.Value;

                right = expected;
            }
            // ReSharper restore PossibleInvalidOperationException

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

            if (leftMonkey.Value == null)
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

        return expected;
    }
}