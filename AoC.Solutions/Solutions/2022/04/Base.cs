using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._04;

public abstract class Base : Solution
{
    public override string Description => "Camp cleanup";

    protected static (Range elf1, Range elf2) ParseLine(string line)
    {
        var pair = line.Split(',');

        var elf1 = new Range(pair[0]);
            
        var elf2 = new Range(pair[1]);

        return (elf1, elf2);
    }
}