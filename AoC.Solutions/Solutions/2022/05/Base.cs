using AoC.Solutions.Infrastructure;
using System.Text;

namespace AoC.Solutions.Solutions._2022._05;

public abstract class Base : Solution
{
    public override string Description => "Supply stacks";

    private Stack<char>[] _stacks;

    private void ParseStacks()
    {
        var index = 0;

        while (! string.IsNullOrWhiteSpace(Input[index]))
        {
            index++;
        }

        index--;

        var count = int.Parse(Input[index].Split(' ', StringSplitOptions.RemoveEmptyEntries).Last());

        var stacks = new Stack<char>[count];

        index--;

        while (index > -1)
        {
            for (var x = 0; x < count; x++)
            {
                stacks[x] ??= new Stack<char>();

                var crate = Input[index][1 + x * 4];

                if (! char.IsLetter(crate))
                {
                    continue;
                }

                stacks[x].Push(crate);
            }

            index--;
        }

        _stacks = stacks;
    }

    private void DoMove(string instruction)
    {
        var parts = instruction.Split(' ', StringSplitOptions.TrimEntries);

        for (var i = 0; i < int.Parse(parts[1]); i++)
        {
            _stacks[int.Parse(parts[5]) - 1].Push(_stacks[int.Parse(parts[3]) - 1].Pop());
        }
    }

    private void DoMoveInOrder(string instruction)
    {
        var parts = instruction.Split(' ', StringSplitOptions.TrimEntries);

        var tempStack = new Stack<char>();

        for (var i = 0; i < int.Parse(parts[1]); i++)
        {
            tempStack.Push(_stacks[int.Parse(parts[3]) - 1].Pop());
        }

        for (var i = 0; i < int.Parse(parts[1]); i++)
        {
            _stacks[int.Parse(parts[5]) - 1].Push(tempStack.Pop());
        }
    }

    protected string MoveCratesAndGetAnswer(bool moveInOrder = false)
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
            if (moveInOrder)
            {
                DoMoveInOrder(Input[index]);
            }
            else
            {
                DoMove(Input[index]);
            }

            index++;
        }

        var result = new StringBuilder();

        foreach (var stack in _stacks)
        {
            result.Append(stack.Pop());
        }

        return result.ToString();
    }
}