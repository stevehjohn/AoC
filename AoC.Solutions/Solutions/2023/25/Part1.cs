using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._25;

[UsedImplicitly]
public class Part1 : Base
{
    private List<(string L, string R)> _nodes;

    private Dictionary<string, List<string>> _links;
    
    private List<string> _distinct;

    public override string GetAnswer()
    {
        var left = 0;

        int right;
        
        ParseInput();

        var backup = _nodes.ToList();

        var links = _links.ToDictionary(l => l.Key, l => l.Value.ToList());

        var rng = new Random();

        while (true)
        {
            var l = _distinct[rng.Next(_distinct.Count)];
            var r = _distinct[rng.Next(_distinct.Count)];

            if (l == r)
            {
                continue;
            }

            Walk(l, r);
            
            Walk(l, r);
            
            Walk(l, r);

            Walk(l, r);

            Walk(l, r);
            
            var result = Walk(l, r);

            if (result < Input.Length && result > 1)
            {
                if (left == 0)
                {
                    left = result;
                    
                    continue;
                }

                if (result != left)
                {
                    right = result;

                    break;
                }
            }

            _nodes = backup.ToList();
            
            _links = links.ToDictionary(i => i.Key, i => i.Value.ToList());
        }

        return (left * right).ToString();
    }

    private int Walk(string start, string end)
    {
        var queue = new Queue<(string Name, List<(string L, string R)> History)>();

        queue.Enqueue((start, []));

        var visited = new HashSet<string>();

        while (queue.TryDequeue(out var node))
        {
            if (! visited.Add(node.Name))
            {
                continue;
            }

            if (node.Name == end)
            {
                node.History.ForAll((_, n) =>
                {
                    _nodes.Remove(n);

                    _links[n.L].Remove(n.R);
                });

                break;
            }

            foreach (var connection in _links[node.Name])
            {
                queue.Enqueue((connection, [..node.History, (node.Name, connection)]));
            }
        }

        return visited.Count;
    }

    private void ParseInput()
    {
        _nodes = [];

        _distinct = [];

        foreach (var line in Input)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);

            _distinct.Add(parts[0]);

            var connections = parts[1].Split(' ', StringSplitOptions.TrimEntries);

            foreach (var connection in connections)
            {
                _nodes.Add((parts[0], connection));

                _nodes.Add((connection, parts[0]));

                if (! _distinct.Contains(connection))
                {
                    _distinct.Add(connection);
                }
            }
        }

        _links = new Dictionary<string, List<string>>();

        foreach (var node in _nodes)
        {
            if (_links.TryGetValue(node.L, out List<string> value))
            {
                value.Add(node.R);
            }
            else
            {
                _links.Add(node.L, [node.R]);
            }
        }
    }
}