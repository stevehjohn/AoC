using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._09;

public abstract class Base : Solution
{
    public override string Description => "Mirage maintenance";
    
    protected readonly List<List<long>> Sequences = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToList();
            
            Sequences.Add(parts);
        }
    }
}