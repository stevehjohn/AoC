using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = CountGroups();

        return result.ToString();
    }

    private int CountGroups()
    {
        var visited = new HashSet<int>();

        var groups = 0;

        var start = Nodes.FirstOrDefault(n => ! visited.Contains(n.Key)).Value;

        while (start != null)
        {
            groups++;

            var queue = new Queue<Node>();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (! visited.Add(node.Id))
                {
                    continue;
                }

                foreach (var connection in node.Connections)
                {
                    if (! visited.Contains(connection.Id))
                    {
                        queue.Enqueue(connection);
                    }
                }
            }

            start = Nodes.FirstOrDefault(n => ! visited.Contains(n.Key)).Value;
        }

        return groups;
    }
}