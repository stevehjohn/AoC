using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._23;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<string> _loops = [];
    
    public override string GetAnswer()
    {
        ParseInput();

        var count = 0;

        foreach (var node in Lan.OrderBy(c => c.Key))
        {
            WalkNodes(node.Key, node.Value, [node.Key], 3);
        }

        foreach (var loop in _loops)
        {
            Console.WriteLine(loop);
        }

        return _loops.Count.ToString();
    }

    private void WalkNodes(string start, Node node, HashSet<string> visited, int steps)
    {
        if (visited.Count > steps)
        {
            return;
        }

        if (visited.Count == steps)
        {
            var result = node.Connections.Any(n => n.Name == start);
            
            if (result)
            {
                _loops.Add(string.Join(" -> ", visited.Order()));
            }

            return;
        }

        foreach (var connection in node.Connections)
        {
            if (visited.Contains(connection.Name))
            {
                continue;
            }

            WalkNodes(start, connection, [..visited, connection.Name], steps);
        }
    }
}