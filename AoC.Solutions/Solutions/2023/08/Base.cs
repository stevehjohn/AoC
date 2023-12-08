using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._08;

public abstract class Base : Solution
{
    public override string Description => "Haunted wasteland";

    private string _steps;
    
    protected readonly Dictionary<string, (string Left, string Right)> Map = new();

    protected void ParseInput()
    {
        _steps = Input[0];

        foreach (var line in Input[2..])
        {
            Map.Add(line.Substring(0, 3), (line.Substring(7, 3), line.Substring(12, 3)));
        }
    }

    protected int WalkMap(string node = "AAA")
    {
        var steps = 0;

        var step = 0;
        
        while (true)
        {
            steps++;
            
            node = _steps[step] == 'L' ? Map[node].Left : Map[node].Right;

            if (node[2] == 'Z')
            {
                break;
            }

            step++;

            if (step == _steps.Length)
            {
                step = 0;
            }
        }

        return steps;
    }
}