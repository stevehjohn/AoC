using System.Text;

namespace AoC.Solutions.Solutions._2022._05;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseStacks();

        var index = 0;

        while (! string.IsNullOrWhiteSpace(Input[index]))
        {
            index++;
        }

        index++;

        while (index < Input.Length)
        {
            DoMove(Input[index]);

            index++;
        }

        var result = new StringBuilder();

        foreach (var stack in Stacks)
        {
            result.Append(stack.Pop());
        }

        return result.ToString();
    }
}