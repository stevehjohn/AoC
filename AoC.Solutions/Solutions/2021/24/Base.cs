using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._24;

public abstract class Base : Solution
{
    public override string Description => "Model number";

    protected string GetModelNumber()
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

            var offset = rule.offset + check;

            if (offset > 0)
            {
                modelNumber[rule.target] = (char) ('9' - offset);

                modelNumber[target++] = '9';
            }
            else
            {
                modelNumber[rule.target] = '9';

                modelNumber[target++] = (char)('9' + offset);
            }
        }

        return new string(modelNumber);
    }

    private int GetOperand(int line)
    {
        return int.Parse(Input[line].Split(' ')[2]);
    }
}