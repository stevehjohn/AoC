namespace AoC.Solutions.Solutions._2019._18;

public class NodeWalker
{
    private readonly Node _node;

    public HashSet<char> Visited { get; }

    private readonly Dictionary<string, string> _doors;

    public int Steps { get; }

    public int VisitedCount => Visited.Count;

    public NodeWalker(Node node, Dictionary<string, string> doors)
    {
        _node = node;

        _doors = doors;

        Visited = new HashSet<char>
                   {
                       node.Name
                   };
    }

    private NodeWalker(NodeWalker previous, Node node, int distance)
    {
        _node = node;

        _doors = previous._doors;

        Visited = new HashSet<char>(previous.Visited)
                   {
                       node.Name
                   };

        Steps = previous.Steps + distance;
    }

    public List<NodeWalker> Walk()
    {
        var newWalkers = new List<NodeWalker>();

        foreach (var (child, distance) in _node.Children)
        {
            if (Visited.Contains(child.Name))
            {
                continue;
            }

            if (IsBlocked(child.Name))
            {
                continue;
            }

            newWalkers.Add(new NodeWalker(this, child, distance));
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
            if (! Visited.Contains(char.ToLower(door)))
            {
                return true;
            }
        }
        
        return false;
    }
}