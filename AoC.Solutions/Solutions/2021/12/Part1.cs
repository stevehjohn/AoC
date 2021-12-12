using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._12;

[UsedImplicitly]
public class Part1 : Solution
{
    private readonly List<Node> _nodes = new();

    private Node _start;

    public override string GetAnswer()
    {
        foreach (var line in Input)
        {
            var caves = line.Split('-');

            var node = _nodes.FirstOrDefault(n => n.Name == caves[0]);

            if (node == null)
            {
                node = new Node(caves[0]);

                _nodes.Add(node);
            }

            var connection = _nodes.FirstOrDefault(c => c.Name == caves[1]);

            if (connection == null)
            {
                connection = new Node(caves[1]);

                _nodes.Add(connection);
            }

            node.Connections.Add(connection);
            
            connection.Connections.Add(node);
        }

        _start = _nodes.Single(n => n.IsStart);

        var pathCount = Visit(_start, new List<Node>());

        return pathCount.ToString();
    }

    private static int Visit(Node node, List<Node> visited)
    {
        if (node.IsEnd)
        {
            return 1;
        }

        if (! node.IsBig && visited.Contains(node))
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