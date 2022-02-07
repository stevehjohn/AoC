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

    private Dictionary<int, Layer> ParseInput()
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

public class Layer
{
    public int Depth { get; }

    public int Position { get; private set; }

    private int _direction = 1;

    public Layer(int depth)
    {
        Depth = depth;
    }

    public void MoveScanner()
    {
        Position += _direction;

        if (Position == 0 || Position == Depth - 1)
        {
            _direction = -_direction;
        }
    }
}