using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._11;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<int> _visited = [];

    private int _paths;

    private int _outId;
    
    public override string GetAnswer()
    {
        ParseInput();

        var you = Nodes[NodeIds["you"]];
        
        _visited.Add(you.Id);

        _outId = NodeIds["out"];
        
        CountPaths(you);
        
        return _paths.ToString();
    }

    private void CountPaths(Node from)
    {
        if (from.Id == _outId)
        {
            _paths++;
            
            return;
        }

        foreach (var connection in from.Connections)
        {
            if (! _visited.Add(connection.Id))
            {
                continue;
            }

            CountPaths(connection);

            _visited.Remove(connection.Id);
        }        
    }
}