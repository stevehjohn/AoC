using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._12;

public abstract class Base : Solution
{
    public override string Description => "Caving";

    private readonly List<Node> _nodes = [];

    protected Node Start;

    protected void LoadMap()
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

        Start = _nodes.Single(n => n.IsStart);
    }
}