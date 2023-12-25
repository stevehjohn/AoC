using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._25;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<string, Node> _nodes = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        TrySplitGroups();
        
        return "Unknown";
    }

    private void TrySplitGroups()
    {
        for (var i = 0; i < _nodes.Count; i++)
        {
            for (var j = i + 1; j < _nodes.Count; j++)
            {
                for (var k = j + 1; k < _nodes.Count; k++)
                {
                }
            }
        }
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);

            var node = new Node
            {
                Name = parts[0]
            };

            _nodes.TryAdd(node.Name, node);

            var connections = parts[1].Split(' ', StringSplitOptions.TrimEntries);

            foreach (var connection in connections)
            {
                node.Connections.Add(connection);

                if (_nodes.TryGetValue(connection, out var connectedNode))
                {
                    connectedNode.Connections.Add(node.Name);
                }
                else
                {
                    _nodes.Add(connection, new Node
                    {
                        Name = connection,
                        Connections = { node.Name }
                    });
                }
            }
        }
    }
}