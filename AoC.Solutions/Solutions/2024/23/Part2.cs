using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._23;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<HashSet<int>> _cliques = [];
    
    public override string GetAnswer()
    {
        ParseInput();

        foreach (var node in Lan)
        {
            var clique = new HashSet<int> { node.Key };
            
            _cliques.Add(clique);
            
            BuildClique(clique, node.Value);
        }

        var longest = _cliques.MaxBy(c => c.Count).Order().Select(n => $"{(char) ((n >> 8) + 'a')}{(char) ((n & 255) + 'a')}");

        return string.Join(',', longest);
    }

    private void BuildClique(HashSet<int> clique, Node node)
    {
        foreach (var connection in node.Connections)
        {
            var connected = true;
            
            foreach (var name in clique)
            {
                if (Lan[name].Connections.All(c => c.Name != connection.Name))
                {
                    connected = false;
                    
                    break;
                }
            }

            if (connected)
            {
                clique.Add(connection.Name);

                BuildClique(clique, connection);
            }
        }
    }
}