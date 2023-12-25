using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._25;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<(string L, string R)> _nodes = new();
    
    public override string GetAnswer()
    {
        ParseInput();
        
        Console.WriteLine(Walk());
        
        return "Unknown";
    }

    private int Walk()
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

            var connections = parts[1].Split(' ', StringSplitOptions.TrimEntries);

            foreach (var connection in connections)
            {
                _nodes.Add((parts[0], connection));

                _nodes.Add((connection, parts[0]));
            }
        }
    }
}