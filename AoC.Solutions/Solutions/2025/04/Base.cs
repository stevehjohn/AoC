using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._04;

public abstract class Base : Solution
{
    public override string Description => "Printing department";
    
    protected readonly Map Map;

    protected Base()
    {
        Map = new Map(Input);
    }
}