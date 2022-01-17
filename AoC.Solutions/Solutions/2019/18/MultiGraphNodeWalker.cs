using System.Text;

namespace AoC.Solutions.Solutions._2019._18;

public class MultiGraphNodeWalker : INodeWalker
{
    public HashSet<char> Visited { get; }

    private readonly Graph[] _graphs;

    public int Steps { get; }

    public int VisitedCount => Visited.Count;

    public bool IsGraphSwitch { get; }

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

    private readonly int _graphIndex;

    private readonly Node[] _graphNodes;

    public MultiGraphNodeWalker(Graph[] graphs, int startGraphIndex)
    {
        _graphs = graphs;
        
        _graphNodes = new Node[_graphs.Length];

        for (var i = 0; i < graphs.Length; i++)
        {
            _graphNodes[i] = _graphs[i].Nodes.Single(kvp => char.IsNumber(kvp.Key)).Value;
        }

        _graphIndex = startGraphIndex;

        Visited = new HashSet<char>
                   {
                       _graphNodes[_graphIndex].Name
                   };

        _allVisited = new List<char>
                      {
                          _graphNodes[_graphIndex].Name
                      };
    }

    private MultiGraphNodeWalker(MultiGraphNodeWalker previous, Node node, int distance)
    {
        _graphs = previous._graphs;
     
        _graphNodes = previous._graphNodes.ToArray();

        _graphIndex = previous._graphIndex;
       
        _graphNodes[_graphIndex] = node;

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

    private MultiGraphNodeWalker(MultiGraphNodeWalker previous, int newGraphIndex)
    {
        _graphs = previous._graphs;
     
        _graphNodes = previous._graphNodes.ToArray();

        _graphIndex = newGraphIndex;

        Visited = new HashSet<char>(previous.Visited)
                  {
                      (char) ('1' + newGraphIndex)
                  };

        _allVisited = new List<char>(previous._allVisited)
                      {
                          (char) ('1' + newGraphIndex)
                      };

        Steps = previous.Steps;

        IsGraphSwitch = true;
    }

    public List<INodeWalker> Walk()
    {
        var newWalkers = new List<INodeWalker>();

        var alternatesAdded = false;

        foreach (var (child, distance) in _graphNodes[_graphIndex].Children)
        {
            if (Visited.Contains(child.Name))
            {
                continue;
            }

            if (IsBlocked(child.Name) && ! alternatesAdded)
            {
                newWalkers.AddRange(AddAlternateGraphWalkers());

                alternatesAdded = true;

                continue;
            }

            if (! _graphs[_graphIndex].Doors.TryGetValue($"{child.Name}{_graphNodes[_graphIndex].Name}", out var blockers))
            {
                _graphs[_graphIndex].Doors.TryGetValue($"{_graphNodes[_graphIndex].Name}{child.Name}", out blockers);
            }

            if (blockers != null)
            {
                foreach (var blocker in blockers)
                {
                    _allVisited.Add(blocker);
                }
            }

            newWalkers.Add(new MultiGraphNodeWalker(this, child, distance));

            //newWalkers.AddRange(AddAlternateGraphWalkers());
        }

        return newWalkers;
    }

    private List<INodeWalker> AddAlternateGraphWalkers()
    {
        var newWalkers = new List<INodeWalker>();

        for (var i = 0; i < _graphs.Length; i++)
        {
            if (i == _graphIndex)
            {
                continue;
            }

            newWalkers.Add(new MultiGraphNodeWalker(this, i));
        }

        return newWalkers;
    }

    private bool IsBlocked(char target)
    {
        if (! _graphs[_graphIndex].Doors.TryGetValue($"{target}{_graphNodes[_graphIndex].Name}", out var blockers))
        {
            _graphs[_graphIndex].Doors.TryGetValue($"{_graphNodes[_graphIndex].Name}{target}", out blockers);
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