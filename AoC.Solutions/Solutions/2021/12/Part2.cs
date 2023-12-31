using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        LoadMap();

        var pathCount = Visit(Start, new List<int>());

        return pathCount.ToString();
    }

    private static int Visit(Node node, List<int> visited, Node specialNode = null)
    {
        if (node.IsEnd)
        {
            return 1;
        }

        if (! node.IsBig && visited.Contains(node.Id))
        {
            if (specialNode != null && node != specialNode)
            {
                return 0;
            }

            if (specialNode == null)
            {
                specialNode = node;
            }
            else
            {
                return 0;
            }
        }

        visited.Add(node.Id);

        var total = 0;

        foreach (var connection in node.Connections)
        {
            if (connection.IsStart)
            {
                continue;
            }

            total += Visit(connection, visited, specialNode);
        }

        visited.Remove(node.Id);

        return total;
    }
}