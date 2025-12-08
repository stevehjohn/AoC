using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._16;

public abstract class Base : Solution
{
    public override string Description => "Opcode reverse-engineering";

    protected static int[] ParseRegisters(string line)
    {
        line = line[9..][..^1];

        return line.Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
    }
}