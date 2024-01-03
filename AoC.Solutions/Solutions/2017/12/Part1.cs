using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = CountConnections(Nodes[0]);

        return result.ToString();
    }

    private static int CountConnections(Node root)
    {
        var visited = new HashSet<Node>();

        var queue = new Queue<Node>();

        queue.Enqueue(root);

        var connections = 1;

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (! visited.Add(node))
            {
                continue;
            }

            foreach (var connection in node.Connections)
            {
                if (! visited.Contains(connection))
                {
                    connections++;

                    queue.Enqueue(connection);
                }
            }
        }

        return connections;
    }
}