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

    protected static List<char> ParseLineToReverePolish(string line, bool additionPrecedent = false)
    {
        line = line.Replace(" ", string.Empty);

        var output = new List<char>();

        var operatorStack = new Stack<char>();

        foreach (var c in line)
        {
            if (char.IsNumber(c))
            {
                output.Add(c);

                continue;
            }

            if (c == '+' || c == '*')
            {
                while (operatorStack.Count > 0 && (operatorStack.Peek() == '+' || operatorStack.Peek() == '*' && ! additionPrecedent))
                {
                    output.Add(operatorStack.Pop());
                }

                operatorStack.Push(c);

                continue;
            }

            if (c == '(')
            {
                operatorStack.Push(c);

                continue;
            }

            if (c == ')')
            {
                var p = operatorStack.Pop();

                while (p != '(')
                {
                    output.Add(p);

                    p = operatorStack.Pop();
                }
            }
        }

        while (operatorStack.Count > 0)
        {
            output.Add(operatorStack.Pop());
        }

        return output;
    }
}