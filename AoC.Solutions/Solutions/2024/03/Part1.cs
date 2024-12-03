using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var answer = 0L;

        var program = string.Join(string.Empty, Input);
        
        var index = 0;
        
        while (index < program.Length)
        {
            var result = FindNextInstruction(program, "mul(", index);

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

        return answer.ToString();
    }
}