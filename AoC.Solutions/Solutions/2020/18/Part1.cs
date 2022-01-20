﻿using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = 0L;

        foreach (var line in Input)
        {
            var operations = ParseLine(line);

            result += CalculateResult(operations);
        }

        return result.ToString();
    }

    private static long CalculateResult(List<string> operations)
    {
        var stack = new Stack<long>();

        foreach (var operation in operations)
        {
            var value = operation[0] == 'X' ? stack.Pop() : operation[0] - '0';

            for (var i = 1; i < operation.Length; i += 2)
            {
                if (operation[i] == '+')
                {
                    value += operation[i + 1] == 'X' ? stack.Pop() : operation[i + 1] - '0';
                }
                else
                {
                    value *= operation[i + 1] == 'X' ? stack.Pop() : operation[i + 1] - '0';
                }
            }

            stack.Push(value);
        }

        return stack.Pop();
    }

    private static List<string> ParseLine(string line)
    {
        line = line.Replace(" ", string.Empty);

        var operations = new List<string>();

        while (true)
        {
            var start = line.LastIndexOf('(');

            if (start == -1)
            {
                operations.Add(line);

                break;
            }

            var end = line.IndexOf(')', start);

            var component = line.Substring(start + 1, end - start - 1);

            operations.Add(component);

            line = $"{line[..start]}X{line[(end + 1)..]}";
        }

        return operations;
    }
}