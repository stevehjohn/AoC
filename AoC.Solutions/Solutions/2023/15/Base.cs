using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._15;

public abstract class Base : Solution
{
    public override string Description => "Lens library";

    protected static int Hash(string input)
    {
        var hash = 0;
        
        foreach (var c in input)
        {
            hash += (byte) c;

            hash = (hash << 4) + hash;

            hash &= 255;
        }

        return hash;
    }
    
    protected List<string> ParseInput()
    {
        var parts = Input[0].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        return parts.ToList();
    }
}