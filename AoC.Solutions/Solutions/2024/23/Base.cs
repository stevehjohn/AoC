using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._23;

public abstract class Base : Solution
{
    public override string Description => "LAN party";

    protected readonly Dictionary<int, Node> Lan = [];

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var left = GetId(line[..2]);

            var right = GetId(line[3..]);

            Lan.TryAdd(left, new Node(left));

            Lan.TryAdd(right, new Node(right));
        }

        foreach (var line in Input)
        {
            var left = GetId(line[..2]);

            var right = GetId(line[3..]);

            Lan[left].Connections.Add(Lan[right]);
            
            Lan[right].Connections.Add(Lan[left]);
        }
    }

    private static int GetId(string name)
    {
        return ((name[0] - 'a') << 8) | (name[1] - 'a');
    }
}