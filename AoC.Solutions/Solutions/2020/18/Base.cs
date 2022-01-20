using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._18;

public abstract class Base : Solution
{
    public override string Description => "Operator precedence";

    protected static long CalculateResult(List<char> operations)
    {
        var stack = new Stack<long>();

        foreach (var operation in operations)
        {
            if (char.IsNumber(operation))
            {
                stack.Push(operation - '0');

                continue;
            }

            var left = stack.Pop();

            var right = stack.Pop();

            if (operation == '+')
            {
                stack.Push(left + right);
            }
            else
            {
                stack.Push(left * right);
            }
        }

        return stack.Pop();
    }

    protected static List<char> ParseLine(string line)
    {
        line = line.Replace(" ", string.Empty);

        var operations = new List<char>();

        while (true)
        {
            var start = line.LastIndexOf('(');

            if (start == -1)
            {
                operations.AddRange(ToReversePolish(line));

                break;
            }

            var end = line.IndexOf(')', start);

            var component = line.Substring(start + 1, end - start - 1);

            operations.AddRange(ToReversePolish(component));

            line = $"{line[..start]}{line[(end + 1)..]}";
        }

        return operations;
    }

    private static char[] ToReversePolish(string input)
    {
        var result = new char[input.Length];

        if (input.Length % 2 != 0)
        {
            result[0] = input[0];

            for (var i = 1; i < input.Length - 1; i += 2)
            {
                result[i] = input[i + 1];

                result[i + 1] = input[i];
            }
        }
        else
        {
            for (var i = 0; i < input.Length - 1; i += 2)
            {
                result[i] = input[i + 1];

                result[i + 1] = input[i];
            }
        }

        return result;
    }
}