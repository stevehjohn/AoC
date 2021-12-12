using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        LoadMap();

        var pathCount = Visit(Start, new List<Node>());

        return pathCount.ToString();
    }

    private static int Visit(Node node, List<Node> visited)
    {
        if (node.IsEnd)
        {
            return 1;
        }

        if (!node.IsBig && visited.Contains(node))
        {
            return 0;
        }

        visited.Add(node);

        var total = 0;

        foreach (var connection in node.Connections)
        {
            if (connection.IsStart)
            {
                continue;
            }

            total += Visit(connection, visited.ToList());
        }

        return total;
    }
}