using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._03;

public abstract class Base : Solution
{
    public override string Description => "Mull it over";

    protected static (int Index, string Instruction) FindNextMulInstruction(string line, int index)
    {
        index = line.IndexOf("mul(", index, StringComparison.InvariantCultureIgnoreCase);

        if (index == -1)
        {
            return (-1, null);
        }

        var end = line.IndexOf(")", index + 1, StringComparison.InvariantCultureIgnoreCase);

        return (index, line[index..(end + 1)]);
    }

    protected static int ParseMulInstruction(string instruction)
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