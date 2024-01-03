using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._16;

public abstract class Base : Solution
{
    public override string Description => "Opcode reverse-engineering";

    // ReSharper disable StringLiteralTypo
    protected readonly string[] OpCodes = { "addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori", "setr", "seti", "gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr" };
    // ReSharper restore StringLiteralTypo

    protected static int[] ParseRegisters(string line)
    {
        line = line[9..][..^1];

        return line.Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
    }
}