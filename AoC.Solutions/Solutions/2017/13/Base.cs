using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._13;

public abstract class Base : Solution
{
    public override string Description => "Firewall run";

    protected static (int Severity, bool Caught) GetSeverity(Dictionary<int, Layer> firewall, int delay = 0)
    {
        var position = -delay;

        var end = firewall.Max(l => l.Key);

        var severity = 0;

        var caught = false;

        while (position <= end)
        {
            if (firewall.ContainsKey(position))
            {
                var layer = firewall[position];

                if (layer.Position == 0)
                {
                    severity += position * layer.Depth;

                    caught = true;
                }
            }

            foreach (var item in firewall)
            {
                item.Value.MoveScanner();
            }

            position++;
        }

        return (severity, caught);
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