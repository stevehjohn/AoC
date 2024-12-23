using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._23;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<string> _loops = [];
    
    public override string GetAnswer()
    {
        ParseInput();

        foreach (var node in Lan)
        {
            WalkNodes(node.Key, node.Value, [node.Key], 3);
        }

        return _loops.Count.ToString();
    }

    private void WalkNodes(int start, Node node, HashSet<int> visited, int steps)
    {
        if (visited.Count > steps)
        {
            return;
        }

        if (visited.Count == steps)
        {
            var result = node.Connections.Any(n => n.Id == start) && visited.Any(n => n >> 8 == 19);
            
            if (result)
            {
                _loops.Add(string.Join(",", visited.Order()));
            }

            return;
        }

        foreach (var connection in node.Connections)
        {
            if (visited.Contains(connection.Id))
            {
                continue;
            }

            WalkNodes(start, connection, [..visited, connection.Id], steps);
        }
    }
}