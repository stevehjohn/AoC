using System.Text;

namespace AoC.Solutions.Solutions._2019._18;

public class MultiGraphNodeWalker : INodeWalker
{
    private readonly Node _node;

    public HashSet<char> Visited { get; }

    private readonly Graph[] _graphs;

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

    public MultiGraphNodeWalker(Node node, Graph[] graphs, int startGraphIndex)
    {
        _node = node;

        _graphs = graphs;

        Visited = new HashSet<char>
                   {
                       node.Name
                   };

        _allVisited = new List<char>
                      {
                          node.Name
                      };
    }

    private MultiGraphNodeWalker(MultiGraphNodeWalker previous, Node node, int distance)
    {
        _node = node;

        _graphs = previous._graphs;

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

            // TODO
            if (! _graphs[0].Doors.TryGetValue($"{child.Name}{_node.Name}", out var blockers))
            {
                // TODO
                _graphs[0].Doors.TryGetValue($"{_node.Name}{child.Name}", out blockers);
            }

            if (blockers != null)
            {
                foreach (var blocker in blockers)
                {
                    _allVisited.Add(blocker);
                }
            }

            newWalkers.Add(new MultiGraphNodeWalker(this, child, distance));
        }

        return newWalkers;
    }
    
    private bool IsBlocked(char target)
    {
        // TODO
        if (! _graphs[0].Doors.TryGetValue($"{target}{_node.Name}", out var blockers))
        {
            // TODO
            _graphs[0].Doors.TryGetValue($"{_node.Name}{target}", out blockers);
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