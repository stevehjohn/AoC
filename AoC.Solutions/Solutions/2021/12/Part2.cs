using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        LoadMap();

        var pathCount = Visit(Start, new Dictionary<Node, int>());

        return pathCount.ToString();
    }

    private static int Visit(Node node, Dictionary<Node, int> visited)
    {
        if (node.IsEnd)
        {
            return 1;
        }

        if (! node.IsBig && visited.ContainsKey(node))
        {
            if (visited.Any(v => v.Value > 0))
            {
                return 0;
            }

            visited[node]++;
        }

        visited.TryAdd(node, 0);

        var total = 0;

        foreach (var connection in node.Connections)
        {
            if (connection.IsStart)
            {
                continue;
            }

            total += Visit(connection, visited.ToDictionary(v => v.Key, v => v.Value));
        }

        return total;
    }
}