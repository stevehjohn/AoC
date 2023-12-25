using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._25;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<string, Node> _nodes = new();

    private readonly List<string> _nodeKeys = new();

    private readonly List<(string, string)> _pairs = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        GeneratePairs();
        
        TrySplitGroups();
        
        return "Unknown";
    }

    private void GeneratePairs()
    {
        for (var i = 0; i < _nodeKeys.Count; i++)
        {
            for (var j = 0; j < _nodeKeys.Count; j++)
            {
                _pairs.Add((_nodeKeys[i], _nodeKeys[j]));
            }
        }
    }

    private void TrySplitGroups()
    {
        for (var i = 0; i < _pairs.Count; i++)
        {
            for (var j = 0; j < _pairs.Count; j++)
            {
                for (var k = 0; k < _pairs.Count; k++)
                {
                    var result = CheckSplit(new List<(string, string)> { _pairs[i], _pairs[j], _pairs[k] });

                    if (result != null)
                    {
                        Console.WriteLine($"Split: {result.Value.Item1} {result.Value.Item2}");
                    }
                }
            }
        }
    }

    private (int, int)? CheckSplit(List<(string, string)> ignore)
    {
        foreach (var item in ignore)
        {
            var g1 = CheckGroupSize(item.Item1, ignore);

            var g2 = CheckGroupSize(item.Item2, ignore);

            if (g1 != _nodeKeys.Count)
            {
                return (g1, g2);
            }
        }

        return null;
    }

    private int CheckGroupSize(string name, List<(string, string)> ignore)
    {
        var visited = new HashSet<string>();

        var queue = new Queue<string>();

        queue.Enqueue(_nodes[name].Connections[0]);

        var size = 0;
        
        while (queue.TryDequeue(out var key))
        {
            foreach (var node in _nodes[key].Connections)
            {
                if (ignore.Contains((key, node)) || ignore.Contains((node, key)))
                {
                    continue;
                }

                if (visited.Add(node))
                {
                    size++;
            
                    queue.Enqueue(node);
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