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
        var rules = new Stack<(int target, int offset)>();

        var modelNumber = new char[14];

        var target = 0;

        for (var subroutine = 0; subroutine < Input.Length; subroutine += 18)
        {
            var check = GetOperand(subroutine + 5);

            if (check > 0)
            {
                rules.Push((target++, GetOperand(subroutine + 15)));

                continue;
            }

            var rule = rules.Pop();

            var totalOffset = rule.offset + check;

            if (totalOffset > 0)
            {
                modelNumber[rule.target] = (char) ('9' - totalOffset);

                modelNumber[target++] = '9';
            }
            else
            {
                modelNumber[rule.target] = '9';

                modelNumber[target++] = (char) ('9' + totalOffset);
            }
        }

        return new string(modelNumber);
    }

    private int GetOperand(int line)
    {
        return int.Parse(Input[line].Split(' ')[2]);
    }
}