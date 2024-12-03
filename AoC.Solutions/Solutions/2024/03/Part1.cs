using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var answer = 0L;
        
        var index = 0;

        while (index < Input[0].Length)
        {
            var result = FindNextMulInstruction(index);
            
            if (result.Index == -1)
            {
                break;
            }

            index = result.Index + 1;

            var returnValue = ParseMulInstruction(result.Instruction);

            if (returnValue == int.MinValue)
            {
                continue;
            }

            answer += returnValue;
        }

        return answer.ToString();
    }

    private (int Index, string Instruction) FindNextMulInstruction(int index)
    {
        index = Input[0].IndexOf("mul", index, StringComparison.InvariantCultureIgnoreCase);

        if (index == -1)
        {
            return (-1, null);
        }

        var end = Input[0].IndexOf(")", index + 1, StringComparison.InvariantCultureIgnoreCase);

        return (index, Input[0][index..(end + 1)]);
    }

    private static int ParseMulInstruction(string instruction)
    {
        instruction = instruction[4..^1];

        var parts = instruction.Split(',', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
        {
            return int.MinValue;
        }

        if (! int.TryParse(parts[0], out var left) || ! int.TryParse(parts[1], out var right))
        {
            return int.MinValue;
        }

        return left * right;
    }
}