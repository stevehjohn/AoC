using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        LoadMap();

        var pathCount = Visit(Start, []);

        return pathCount.ToString();
    }

    private static int Visit(Node node, List<int> visited)
    {
        if (node.IsEnd)
        {
            return 1;
        }

        if (! node.IsBig && visited.Contains(node.Id))
        {
            return 0;
        }

        visited.Add(node.Id);

        var total = 0;

        foreach (var connection in node.Connections)
        {
            if (connection.IsStart)
            {
                continue;
            }

            total += Visit(connection, visited);
        }

        visited.Remove(node.Id);

        return total;
    }
}