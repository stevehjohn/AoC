namespace AoC.Solutions.Solutions._2019._18;

public class Graph
{
    private readonly Dictionary<char, Node> _nodes = new();

    private Dictionary<string, int> _distances;

    public void Build(Dictionary<string, int> distances)
    {
        _distances = distances;

        _nodes.Add('@', new Node('@'));

        foreach (var c in _distances.Select(d => char.ToLower(d.Key[1])).Distinct())
        {
            _nodes.Add(c, new Node(c));
        }

        foreach (var (parentKey, node) in _nodes)
        {
            var connections = _distances.Where(d => d.Key.Contains(parentKey));

            foreach (var (childKey, distance) in connections)
            {
                var child = childKey.Replace(parentKey.ToString(), string.Empty)[0];

                if (char.IsUpper(child))
                {
                    continue;
                }

                node.Children.Add(_nodes[child], distance);
            }
        }
    }

    public int Solve()
    {
        var queue = new Queue<NodeWalker>();

        queue.Enqueue(new NodeWalker(_nodes['@']));

        var minSteps = int.MaxValue;

        while (queue.Count > 0)
        {
            var walker = queue.Dequeue();

            if (walker.Steps > minSteps)
            {
                continue;
            }

            var newWalkers = walker.Walk();

            if (newWalkers.Count == 0)
            {
                if (walker.Steps < minSteps)
                {
                    minSteps = walker.Steps;
                }

                continue;
            }

            newWalkers.ForEach(w => queue.Enqueue(w));
        }

        return minSteps;
    }
}

public class Node
{
    public char Name { get; }

    public Dictionary<Node, int> Children { get; }

    public Node(char name)
    {
        Name = name;

        Children = new Dictionary<Node, int>();
    }
}

public class NodeWalker
{
    private readonly Node _node;

    private readonly HashSet<char> _visited;

    public int Steps { get; private set; }

    public NodeWalker(Node node)
    {
        _node = node;

        _visited = new HashSet<char>
                   {
                       node.Name
                   };
    }

    public NodeWalker(NodeWalker previous, Node node)
    {
        _node = node;

        _visited = new HashSet<char>(previous._visited);

        Steps = previous.Steps;
    }

    public List<NodeWalker> Walk()
    {
        var newWalkers = new List<NodeWalker>();

        foreach (var (child, distance) in _node.Children)
        {
            if (_visited.Contains(child.Name))
            {
                continue;
            }

            _visited.Add(child.Name);

            Steps += distance;

            newWalkers.Add(new NodeWalker(this, child));
        }

        return newWalkers;
    }
}