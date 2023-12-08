using JetBrains.Annotations;
using TraceReloggerLib;

namespace AoC.Solutions.Solutions._2023._08;

[UsedImplicitly]
public class Part1 : Base
{
    private string _steps;
    
    private Dictionary<string, (string Left, string Right)> _map = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        var result = WalkMap();
        
        return result.ToString();
    }

    private int WalkMap()
    {
        var steps = 0;

        var step = 0;

        var node = "AAA";

        while (node != "ZZZ")
        {
            steps++;
            
            node = _steps[step] == 'L' ? _map[node].Left : _map[node].Right;
            
            step++;

            if (step == _steps.Length)
            {
                step = 0;
            }
        }

        return steps;
    }

    private void ParseInput()
    {
        _steps = Input[0];

        foreach (var line in Input[2..])
        {
            _map.Add(line.Substring(0, 3), (line.Substring(7, 3), line.Substring(12, 3)));
        }
    }
}