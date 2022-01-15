using System.Diagnostics;
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public class Graph
{
    private readonly Dictionary<char, Node> _nodes = new();

    private Dictionary<string, int> _distances;

    private Dictionary<string, List<Point>> _paths;

    private Dictionary<char, Point> _itemLocations;

    public void Build(Dictionary<string, int> distances, Dictionary<string, List<Point>> paths, Dictionary<char, Point> itemLocations)
    {
        _distances = distances;

        _paths = paths;

        _itemLocations = itemLocations;

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
        var queue = new PriorityQueue<NodeWalker, int>();

        queue.Enqueue(new NodeWalker(_nodes['@'], _paths, _itemLocations), 0);

        var minSteps = int.MaxValue;

        var sw = Stopwatch.StartNew();

        while (queue.Count > 0)
        {
            var walker = queue.Dequeue();

            if (walker.Steps > minSteps)
            {
                continue;
            }

            var newWalkers = walker.Walk();

            if (newWalkers.Count == 0 && walker.VisitedCount == 27)
            {
                if (walker.Steps < minSteps)
                {
                    minSteps = walker.Steps;
                }

                continue;
            }

            newWalkers.ForEach(w => queue.Enqueue(w, int.MaxValue - w.VisitedCount));
        }

        sw.Stop();

        Console.WriteLine(sw.Elapsed);

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

    private readonly Dictionary<string, List<Point>> _paths;

    private readonly Dictionary<char, Point> _itemLocations;

    public int Steps { get; private set; }

    public int VisitedCount => _visited.Count;

    public NodeWalker(Node node, Dictionary<string, List<Point>> paths, Dictionary<char, Point> itemLocations)
    {
        _node = node;

        _paths = paths;

        _itemLocations = itemLocations;

        _visited = new HashSet<char>
                   {
                       node.Name
                   };
    }

    public NodeWalker(NodeWalker previous, Node node)
    {
        _node = node;

        _paths = previous._paths;

        _itemLocations = previous._itemLocations;

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

            if (IsBlocked(child.Name))
            {
                continue;
            }

            _visited.Add(child.Name);

            Steps += distance;

            newWalkers.Add(new NodeWalker(this, child));
        }

        return newWalkers;
    }

    private bool IsBlocked(char target)
    {
        var pathKey = new string(new[] { _node.Name, target }.OrderBy(x => x).ToArray());

        var path = _paths[pathKey];

        foreach (var itemLocation in _itemLocations)
        {
            if (itemLocation.Key < 'a' && ! _visited.Contains(char.ToLower(itemLocation.Key)))
            {
                if (path.Contains(itemLocation.Value))
                {
                    return false;
                }
            }
        }

        return true;
    }
}