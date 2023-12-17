using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var firewall = ParseInput();

        var result = GetSeverity(firewall);

        return result.ToString();
    }

    private static int GetSeverity(Dictionary<int, Layer> firewall)
    {
        var position = 0;

        var end = firewall.Max(l => l.Key);

        var severity = 0;

        while (position <= end)
        {
            if (firewall.TryGetValue(position, out var layer))
            {
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
}