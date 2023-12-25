using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._25;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<(string L, string R)> _nodes = new();

    private readonly HashSet<string> _distinct = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        Solve();
        
        return "Unknown";
    }

    private int Solve()
    {
        for (var i = 0; i < _nodes.Count; i++)
        {
            for (var j = i + 1; j < _nodes.Count; j++)
            {
                for (var k = j + 1; k < _nodes.Count; k++)
                {
                    var count = Walk(new List<(string L, string R)> { _nodes[i], _nodes[j], _nodes[k] }); 
                    
                    if (count < _distinct.Count)
                    {
                        Console.WriteLine($"YO: {_nodes[i]}, {_nodes[j]}, {_nodes[k]}");

                        return count;
                    }
                }
            }
        }

        return _distinct.Count;
    }

    private int Walk(List<(string L, string R)> ignore)
    {
        var queue = new Queue<string>();
    
        queue.Enqueue(_nodes[0].L);

        var visited = new HashSet<string>();
        
        while (queue.TryDequeue(out var node))
        {
            if (! visited.Add(node))
            {
                continue;
            }

            foreach (var connection in _nodes.Where(n => n.L == node).ToList())
            {
                if (ignore.Contains((node, connection.R)))
                {
                    continue;
                }
                
                queue.Enqueue(connection.R);
            }
        }

        return visited.Count;
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);

            _distinct.Add(parts[0]);
            
            var connections = parts[1].Split(' ', StringSplitOptions.TrimEntries);

            foreach (var connection in connections)
            {
                _nodes.Add((parts[0], connection));

                _nodes.Add((connection, parts[0]));

                _distinct.Add(connection);
            }
        }
    }
}