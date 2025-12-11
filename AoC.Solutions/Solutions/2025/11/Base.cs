using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._11;

public abstract class Base : Solution
{
    public override string Description => "Reactor";

    protected readonly Dictionary<string, Node> Nodes = [];

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var name = line[..3];
            
            if (! Nodes.TryGetValue(name, out var node))
            {
                node = new Node(name);
                
                Nodes.Add(name, node);
            }

            var connections = line[4..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            foreach (var connection in connections)
            {
                if (! Nodes.TryGetValue(connection, out var value))
                {
                    value = new Node(connection);
                    
                    Nodes.Add(connection, value);
                }
                
                node.Connections.Add(value);

                if (! value.Connections.Contains(node))
                {
                    value.Connections.Add(node);
                }
            }
        }
    }
}