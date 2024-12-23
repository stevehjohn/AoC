using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._23;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<string> _visited = [];
    
    public override string GetAnswer()
    {
        ParseInput();

        var count = 0;

        foreach (var node in Lan)
        {
            _visited.Clear();
            
            count += WalkNodes(node.Key, node.Value.Connections, 2) ? 1 : 0;
        }

        return count.ToString();
    }

    private bool WalkNodes(string start, List<Node> connections, int depth)
    {
        foreach (var connection in connections)
        {
            if (! _visited.Add(connection.Name))
            {
                continue;
            }

            if (connection.Name == start && depth == 0)
            {
                return true;
            }

            if (depth > 0)
            {
                if (WalkNodes(start, Lan[connection.Name].Connections, depth - 1))
                {
                    return true;
                }
            }
        }

        return false;
    }
}