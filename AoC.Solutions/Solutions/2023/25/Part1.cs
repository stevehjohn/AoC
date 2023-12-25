using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._25;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<string, Node> _nodes = new();

    private readonly List<string> _nodeKeys = new();
    
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
                    CheckSplit(new List<string> { _nodeKeys[i], _nodeKeys[j], _nodeKeys[k] });
                }
            }
        }
    }

    private void CheckSplit(List<string> ignore)
    {
        foreach (var item in ignore)
        {
            Console.Write($"{CheckGroupSize(item, ignore)} ");
        }
        
        Console.WriteLine();
    }

    private int CheckGroupSize(string name, List<string> ignore)
    {
        var visited = new HashSet<string>();

        var queue = new Queue<string>();

        queue.Enqueue(_nodes[name].Connections[0]);

        var size = 0;
        
        while (queue.TryDequeue(out var key))
        {
            size++;
            
            foreach (var node in _nodes[key].Connections)
            {
                if (! ignore.Contains(node))
                {
                    if (visited.Add(node))
                    {
                        queue.Enqueue(node);
                    }
                }
            }
        }

        return size;
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

            if (! _nodeKeys.Contains(node.Name))
            {
                _nodeKeys.Add(node.Name);
            }

            var connections = parts[1].Split(' ', StringSplitOptions.TrimEntries);

            foreach (var connection in connections)
            {
                if (! _nodeKeys.Contains(connection))
                {
                    _nodeKeys.Add(connection);
                }

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