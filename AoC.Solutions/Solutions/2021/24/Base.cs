using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._24;

public abstract class Base : Solution
{
    public override string Description => "Model number";

    /*
     * Won't pretend to understand this.
     * Cribbed from this: https://www.reddit.com/r/adventofcode/comments/rnejv5/2021_day_24_solutions/hptwvcz/?utm_source=reddit&utm_medium=web2x&context=3
     */
    protected string GetModelNumber(bool max)
    {
        var baseChar = max ? '9' : '1';

        var rules = new Stack<(int target, int offset)>();

        var modelNumber = new string(baseChar, 14).ToCharArray();

        var target = 0;

        for (var subroutine = 0; subroutine < Input.Length; subroutine += 18)
        {
            var operand = GetOperand(subroutine + 5);

            if (operand > 0)
            {
                rules.Push((target++, GetOperand(subroutine + 15)));

                continue;
            }

            var rule = rules.Pop();

            var offset = rule.offset + operand;

            if (offset > 0)
            {
                if (max)
                {
                    modelNumber[rule.target] -= (char) offset;
                }
                else
                {
                    modelNumber[target] += (char) offset;
                }
            }
            else
            {
                if (max)
                {
                    modelNumber[target] += (char) offset;
                }
                else
                {
                    modelNumber[rule.target] -= (char) offset;
                }
            }

            target++;
        }

        return new string(modelNumber);
    }

    private int GetOperand(int line)
    {
        return int.Parse(Input[line].Split(' ')[2]);
    }
}