using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._13;

public abstract class Base : Solution
{
    public override string Description => "Firewall run";

    protected Dictionary<int, Layer> ParseInput()
    {
        var firewall = new Dictionary<int, Layer>();

        foreach (var line in Input)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);

            firewall.Add(int.FastParse(parts[0]), new Layer(int.FastParse(parts[1])));
        }

        return firewall;
    }
}