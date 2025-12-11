using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._11;

[UsedImplicitly]
public class Part1 : Base
{
    private bool[] _visited;

    private int _paths;

    private int _outId;
    
    public override string GetAnswer()
    {
        ParseInput();

        _visited = new bool[NodeIds.Count];
        
        var you = Nodes[NodeIds["you"]];

        _visited[you.Id] = true;

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
            if (_visited[connection.Id])
            {
                continue;
            }

            _visited[connection.Id] = true;

            CountPaths(connection);

            _visited[connection.Id] = false;
        }        
    }
}