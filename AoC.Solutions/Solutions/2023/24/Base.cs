using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._24;

public abstract class Base : Solution
{
    public override string Description => "Never tell me the odds";

    protected readonly List<(DoublePoint Position, DoublePoint Velocity)> Hail = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split('@', StringSplitOptions.TrimEntries);

            Hail.Add((DoublePoint.Parse(parts[0]), DoublePoint.Parse(parts[1])));
        }
    }
}