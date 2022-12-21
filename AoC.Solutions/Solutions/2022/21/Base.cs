using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._21;

public abstract class Base : Solution
{
    public override string Description => "Monkey math";

    protected const string RootMonkeyName = "root";

    protected const string HumanMonkeyName = "humn";

    protected readonly Dictionary<string, Monkey> Monkeys = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var name = parts[0];

            var rightParts = parts[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var value = 0d;

            string leftName = null;

            string rightName = null;

            string @operator = null;

            if (rightParts.Length == 1)
            {
                value = double.Parse(rightParts[0]);
            }
            else
            {
                leftName = rightParts[0];

                rightName = rightParts[2];

                @operator = rightParts[1];
            }

            Monkeys.Add(name, new Monkey(name, leftName, rightName, @operator, value));
        }
    }

    protected double Solve(string name = RootMonkeyName)
    {
        var monkey = Monkeys[name];

        if (monkey.Operator == null)
        {
            return monkey.Value;
        }

        var leftValue = Solve(monkey.Left);

        var rightValue = Solve(monkey.Right);

        double value;

        switch (monkey.Operator)
        {
            case "-":
                value = leftValue - rightValue;
                break;

            case "/":
                value = leftValue / rightValue;
                break;

            case "*":
                value = leftValue * rightValue;
                break;

            default:
                value = leftValue + rightValue;
                break;
        }

        Monkeys[name].Value = value;

        return value;
    }
}