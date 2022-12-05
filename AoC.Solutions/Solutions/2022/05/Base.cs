using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._05;

public abstract class Base : Solution
{
    public override string Description => "Supply Stacks";

    protected Stack<char>[] Stacks;

    protected void ParseStacks()
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
                if (stacks[x] == null)
                {
                    stacks[x] = new Stack<char>();
                }

                var crate = Input[index][1 + x * 4];

                if (! char.IsLetter(crate))
                {
                    continue;
                }

                stacks[x].Push(crate);
            }

            index--;
        }

        Stacks = stacks;
    }

    protected void DoMove(string instruction)
    {
        var parts = instruction.Split(' ', StringSplitOptions.TrimEntries);

        for (var i = 0; i < int.Parse(parts[1]); i++)
        {
            Stacks[int.Parse(parts[5]) - 1].Push(Stacks[int.Parse(parts[3]) - 1].Pop());
        }
    }
}