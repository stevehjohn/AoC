using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._08;

public abstract class Base : Solution
{
    public override string Description => "Haunted wasteland";

    protected string Steps;
    
    protected readonly Dictionary<string, (string Left, string Right)> Map = new();

    protected void ParseInput()
    {
        Steps = Input[0];

        foreach (var line in Input[2..])
        {
            Map.Add(line.Substring(0, 3), (line.Substring(7, 3), line.Substring(12, 3)));
        }
    }

    protected int WalkMap()
    {
        var steps = 0;

        var step = 0;

        var node = "AAA";
        
        while (true)
        {
            steps++;
            
            node = Steps[step] == 'L' ? Map[node].Left : Map[node].Right;

            if (node[2] == 'Z')
            {
                break;
            }

            step++;

            if (step == Steps.Length)
            {
                step = 0;
            }
        }

        return steps;
    }
}