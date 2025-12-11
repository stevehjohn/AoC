using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._11;

public abstract class Base : Solution
{
    public override string Description => "Reactor";

    protected readonly Dictionary<int, Node> Nodes = [];

    protected readonly Dictionary<string, int> NodeIds = [];

    protected void ParseInput()
    {
        var id = 0;
        
        foreach (var line in Input)
        {
            var name = line[..3];

            Node node;
            
            if (! NodeIds.TryGetValue(name, out _))
            {
                node = new Node(id);
                
                Nodes.Add(id, node);
                
                NodeIds.Add(name, id++);
            }
            else
            {
                node = Nodes[NodeIds[name]];
            }

            var connections = line[4..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            foreach (var connection in connections)
            {
                if (! NodeIds.TryGetValue(connection, out _))
                {
                    var connectedNode = new Node(id);
                
                    Nodes.Add(id, connectedNode);
                
                    NodeIds.Add(connection, id++);
                    
                    node.Connections.Add(connectedNode);
                }
                else
                {
                    var connectedNode = Nodes[NodeIds[connection]];
                
                    node.Connections.Add(connectedNode);
                }
            }
        }
    }
}