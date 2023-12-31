using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._12;

public abstract class Base : Solution
{
    public override string Description => "Path through the pipes";

    protected readonly Dictionary<int, Node> Nodes = new();
    
    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(" <-> ", StringSplitOptions.TrimEntries);

            var id = int.Parse(parts[0]);

            if (! Nodes.TryGetValue(id, out var node))
            {
                node = new Node(id);

                Nodes.Add(node.Id, node);
            }

            var connectionsString = parts[1].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse);

            foreach (var connection in connectionsString)
            {
                if (Nodes.TryGetValue(connection, out var cached))
                {
                    node.Connections.Add(cached);
                }
                else
                {
                    var connectionNode = new Node(connection);

                    node.Connections.Add(connectionNode);

                    Nodes.Add(connectionNode.Id, connectionNode);
                }
            }
        }
    }
}