using System.Text;

namespace AoC.Solutions.Solutions._2019._18;

public class MultiGraphNodeWalker : INodeWalker
{
    public List<char> AllVisited { get; }

    private readonly Graph[] _graphs;

    public int Steps { get; }

    public int VisitedCount => _visited.Count;

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

    private readonly HashSet<char> _visited;

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

        _visited = [_graphNodes[_graphIndex].Name];

        AllVisited = [_graphNodes[_graphIndex].Name];
    }

    private MultiGraphNodeWalker(MultiGraphNodeWalker previous, Node node, int distance)
    {
        _graphs = previous._graphs;
     
        _graphNodes = previous._graphNodes.ToArray();

        _graphIndex = previous._graphIndex;
       
        _graphNodes[_graphIndex] = node;

        _visited = [..previous._visited, node.Name];

        AllVisited = [..previous.AllVisited, node.Name];

        Steps = previous.Steps + distance;
    }

    private MultiGraphNodeWalker(MultiGraphNodeWalker previous, int newGraphIndex)
    {
        _graphs = previous._graphs;
     
        _graphNodes = previous._graphNodes.ToArray();

        _graphIndex = newGraphIndex;

        _visited = [..previous._visited, (char) ('1' + newGraphIndex)];

        AllVisited = [..previous.AllVisited, (char) ('1' + newGraphIndex)];

        Steps = previous.Steps;
    }

    public List<INodeWalker> Walk()
    {
        var newWalkers = new List<INodeWalker>();

        foreach (var (child, distance) in _graphNodes[_graphIndex].Children)
        {
            if (_visited.Contains(child.Name))
            {
                continue;
            }

            if (IsBlocked(child.Name))
            {
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
                    AllVisited.Add(blocker);
                }
            }

            newWalkers.Add(new MultiGraphNodeWalker(this, child, distance));
        }
        
        newWalkers.AddRange(AddAlternateGraphWalkers());

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
            if (! _visited.Contains(char.ToLower(door)))
            {
                return true;
            }
        }
        
        return false;
    }
}