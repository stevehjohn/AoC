using System.Text;

namespace AoC.Solutions.Solutions._2019._18;

public class NodeWalker : INodeWalker
{
    private readonly Node _node;

    private readonly Graph _graph;

    public List<char> AllVisited { get; }
    
    public int Steps { get; }

    public int VisitedCount => Visited.Count;

    public string Signature 
    {
        get
        {
            var builder = new StringBuilder();

            builder.Append(AllVisited.First());

            if (AllVisited.Count > 2)
            {
                var ordered = AllVisited.GetRange(1, AllVisited.Count - 2).Distinct().OrderBy(c => c).ToArray();

                for (var i = 0; i < ordered.Length; i++)
                {
                    builder.Append(ordered[i]);
                }
            }
            
            builder.Append(AllVisited.Last());

            return builder.ToString();
        }
    }

    private HashSet<char> Visited { get; }

    public NodeWalker(Node node, Graph graph)
    {
        _node = node;

        _graph = graph;

        Visited = [node.Name];

        AllVisited = [node.Name];
    }

    private NodeWalker(NodeWalker previous, Node node, int distance)
    {
        _node = node;

        _graph = previous._graph;

        Visited = [..previous.Visited, node.Name];

        AllVisited = [..previous.AllVisited, node.Name];

        Steps = previous.Steps + distance;
    }

    public List<INodeWalker> Walk()
    {
        var newWalkers = new List<INodeWalker>();

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

            if (! _graph.Doors.TryGetValue($"{child.Name}{_node.Name}", out var blockers))
            {
                _graph.Doors.TryGetValue($"{_node.Name}{child.Name}", out blockers);
            }

            if (blockers != null)
            {
                foreach (var blocker in blockers)
                {
                    AllVisited.Add(blocker);
                }
            }

            newWalkers.Add(new NodeWalker(this, child, distance));
        }

        return newWalkers;
    }
    
    private bool IsBlocked(char target)
    {
        if (! _graph.Doors.TryGetValue($"{target}{_node.Name}", out var blockers))
        {
            _graph.Doors.TryGetValue($"{_node.Name}{target}", out blockers);
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