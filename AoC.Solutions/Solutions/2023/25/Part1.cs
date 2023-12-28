using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._25;

[UsedImplicitly]
public class Part1 : Base
{
    private List<(string L, string R)> _nodes;

    private List<string> _distinct;

    public override string GetAnswer()
    {
        int left = 0;

        int right;
        
        ParseInput();

        var backup = _nodes.ToList();

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
        }

        return (left * right).ToString();
    }

    private int Walk(string start, string end, bool remove = true)
    {
        var queue = new Queue<(string Name, List<(string L, string R)> History)>();

        queue.Enqueue((start, new List<(string L, string R)>()));

        var visited = new HashSet<string>();

        while (queue.TryDequeue(out var node))
        {
            if (! visited.Add(node.Name))
            {
                continue;
            }

            if (node.Name == end)
            {
                if (remove)
                {
                    foreach (var item in node.History)
                    {
                        _nodes.Remove(item);
                    }
                }

                break;
            }

            foreach (var connection in _nodes.Where(n => n.L == node.Name).ToList())
            {
                queue.Enqueue((connection.R, new List<(string L, string R)>(node.History) { connection }));
            }
        }

        return visited.Count;
    }

    private void ParseInput()
    {
        _nodes = new List<(string L, string R)>();

        _distinct = new List<string>();

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
    }
}