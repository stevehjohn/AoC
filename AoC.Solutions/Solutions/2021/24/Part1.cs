using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._24;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = GetModelNumber();

        return result;
    }

    private string GetModelNumber()
    {
        var stack = new Stack<(int source, int offset)>();
        
        var modelNumber = new char[14];

        var target = 0;

        for (var subroutine = 0; subroutine < Input.Length; subroutine += 18)
        {
            var check = GetOperand(subroutine + 5);

            var offset = GetOperand(subroutine + 15);

            if (check > 0)
            {
                stack.Push((target++, offset));

                continue;
            }

            var rule = stack.Pop();

            var totalOffset = rule.offset + check;

            if (totalOffset > 0)
            {
                modelNumber[rule.source] = (char) ('9' - (char) totalOffset);

                modelNumber[target++] = '9';
            }
            else
            {
                modelNumber[rule.source] = '9';

                modelNumber[target++] = (char) ('9' + (char) totalOffset);
            }
        }

        return new string(modelNumber);
    }

    private int GetOperand(int line)
    {
        return int.Parse(Input[line].Split(' ')[2]);
    }
}