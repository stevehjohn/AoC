using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._12;

public abstract class Base : Solution
{
    public override string Description => "Caving";

    protected readonly List<Node> Nodes = new();

    protected Node Start;

    public void LoadMap()
    {
        foreach (var line in Input)
        {
            var caves = line.Split('-');

            var node = Nodes.FirstOrDefault(n => n.Name == caves[0]);

            if (node == null)
            {
                node = new Node(caves[0]);

                Nodes.Add(node);
            }

            var connection = Nodes.FirstOrDefault(c => c.Name == caves[1]);

            if (connection == null)
            {
                connection = new Node(caves[1]);

                Nodes.Add(connection);
            }

            node.Connections.Add(connection);

            connection.Connections.Add(node);
        }

        Start = Nodes.Single(n => n.IsStart);
    }
}