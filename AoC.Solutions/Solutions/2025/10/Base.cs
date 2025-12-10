using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._10;

public abstract class Base : Solution
{
    public override string Description => "Puzzle 10";

    protected readonly List<Machine> Machines = [];
    
    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            Machines.Add(new Machine(line));
        }
    }
}