using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._11;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<string> _visited = [];

    private int _paths;
    
    public override string GetAnswer()
    {
        ParseInput();

        _visited.Add("you");
        
        CountPaths(Nodes["you"]);
        
        return _paths.ToString();
    }

    private void CountPaths(Node from)
    {
        if (from.Name == "out")
        {
            _paths++;
            
            return;
        }

        foreach (var connection in from.Connections)
        {
            if (! _visited.Add(connection.Name))
            {
                continue;
            }

            CountPaths(connection);

            _visited.Remove(connection.Name);
        }        
    }
}