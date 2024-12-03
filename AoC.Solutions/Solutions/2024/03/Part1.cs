using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var answer = 0L;

        for (var i = 0; i < Input.Length; i++)
        {
            var index = 0;

            while (index < Input[i].Length)
            {
                var result = FindNextMulInstruction(Input[i], index);

                if (result.Index == -1)
                {
                    break;
                }

                var returnValue = ParseMulInstruction(result.Instruction);

                if (returnValue == int.MinValue)
                {
                    index += 1;

                    continue;
                }

                index = result.Index + 1;

                answer += returnValue;
            }
        }

        return answer.ToString();
    }

    private static (int Index, string Instruction) FindNextMulInstruction(string line, int index)
    {
        index = line.IndexOf("mul(", index, StringComparison.InvariantCultureIgnoreCase);

        if (index == -1)
        {
            return (-1, null);
        }

        var end = line.IndexOf(")", index + 1, StringComparison.InvariantCultureIgnoreCase);

        return (index, line[index..(end + 1)]);
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