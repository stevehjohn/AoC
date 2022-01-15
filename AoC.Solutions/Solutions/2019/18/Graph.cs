using System.Diagnostics;
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public class Graph
{
    private readonly Dictionary<char, Node> _nodes = new();

    private Dictionary<string, string> _doors;

    private Dictionary<string, int> _distances;

    public void Build(Dictionary<string, int> distances, Dictionary<string, string> doors)
    {
        _distances = distances;

        _doors = doors;

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

        queue.Enqueue(new NodeWalker(_nodes['@'], _doors), 0);

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

    private readonly Dictionary<string, string> _doors;

    private readonly Dictionary<char, Point> _itemLocations;

    public int Steps { get; private set; }

    public int VisitedCount => _visited.Count;

    public NodeWalker(Node node, Dictionary<string, string> doors)
    {
        _node = node;

        _doors = doors;

        _visited = new HashSet<char>
                   {
                       node.Name
                   };
    }

    public NodeWalker(NodeWalker previous, Node node)
    {
        _node = node;

        _doors = previous._doors;

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
        if (! _doors.TryGetValue($"{target}{_node.Name}", out var blockers))
        {
            _doors.TryGetValue($"{_node.Name}{target}", out blockers);
        }

        if (blockers == null)
        {
            return false;
        }

        foreach (var door in blockers)
        {
            if (! _visited.Contains(char.ToLower(door)))
            {
                return true;
            }
        }
        
        return false;
    }
}