using System.Text;

namespace AoC.Solutions.Solutions._2019._18;

public class NodeWalker : INodeWalker
{
    private readonly Node _node;

    public HashSet<char> Visited { get; }

    private readonly Graph _graph;

    public int Steps { get; }

    public int VisitedCount => Visited.Count;

    public string Signature 
    {
        get
        {
            var builder = new StringBuilder();

            builder.Append(_allVisited.First());

            if (_allVisited.Count > 2)
            {
                var ordered = _allVisited.GetRange(1, _allVisited.Count - 2).Distinct().OrderBy(c => c).ToArray();

                for (var i = 0; i < ordered.Length; i++)
                {
                    builder.Append(ordered[i]);
                }
            }
            
            builder.Append(_allVisited.Last());

            return builder.ToString();
        }
    }

    private readonly List<char> _allVisited;

    public NodeWalker(Node node, Graph graph)
    {
        _node = node;

        _graph = graph;

        Visited = new HashSet<char>
                   {
                       node.Name
                   };

        _allVisited = new List<char>
                      {
                          node.Name
                      };
    }

    private NodeWalker(NodeWalker previous, Node node, int distance)
    {
        _node = node;

        _graph = previous._graph;

        Visited = new HashSet<char>(previous.Visited)
                   {
                       node.Name
                   };

        _allVisited = new List<char>(previous._allVisited)
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

            if (! _graph.Doors.TryGetValue($"{child.Name}{_node.Name}", out var blockers))
            {
                _graph.Doors.TryGetValue($"{_node.Name}{child.Name}", out blockers);
            }

            if (blockers != null)
            {
                foreach (var blocker in blockers)
                {
                    _allVisited.Add(blocker);
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