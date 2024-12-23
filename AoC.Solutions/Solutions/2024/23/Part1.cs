using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._23;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<(string Left, string Right)> _connections = [];
    
    public override string GetAnswer()
    {
        ParseInput();

        var result = 0;

        foreach (var connection in _connections)
        {
            var leftConnections = _connections.Where(c => c.Right == connection.Left).ToList();

            var rightConnections = _connections.Where(c => c.Left == connection.Right).ToList();

            var union = leftConnections.Union(rightConnections).ToList();

            if (union.Count > 3 && union.Any(c => c.Left[0] == 't' || c.Right[0] == 't'))
            {
                result++;
            
                Console.WriteLine(string.Join(',', union));
            }
        }
        
        return result.ToString();
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            _connections.Add((line[..2], line[3..]));
        }
    }
}