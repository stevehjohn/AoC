using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._23;

[UsedImplicitly]
public class Part1 : Base
{
    private List<(string Left, string Right)> _connections = [];
    
    public override string GetAnswer()
    {
        ParseInput();
        
        return "Unknown";
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            _connections.Add((line[..2], line[3..]));
        }
    }
}