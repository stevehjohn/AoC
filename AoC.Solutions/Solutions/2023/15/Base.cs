using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._15;

public abstract class Base : Solution
{
    public override string Description => "Lens library";

    protected List<string> ParseInput()
    {
        var parts = Input[0].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        return parts.ToList();
    }
}