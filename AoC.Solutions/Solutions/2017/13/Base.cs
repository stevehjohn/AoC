using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._13;

public abstract class Base : Solution
{
    public override string Description => "Firewall run";

    protected static int GetSeverity(Dictionary<int, Layer> firewall)
    {
        var position = 0;

        var end = firewall.Max(l => l.Key);

        var severity = 0;

        while (position <= end)
        {
            if (firewall.ContainsKey(position))
            {
                var layer = firewall[position];

                if (layer.Position == 0)
                {
                    severity += position * layer.Depth;
                }
            }

            foreach (var item in firewall)
            {
                item.Value.MoveScanner();
            }

            position++;
        }

        return severity;
    }

    protected Dictionary<int, Layer> ParseInput()
    {
        var firewall = new Dictionary<int, Layer>();

        foreach (var line in Input)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);

            firewall.Add(int.Parse(parts[0]), new Layer(int.Parse(parts[1])));
        }

        return firewall;
    }
}