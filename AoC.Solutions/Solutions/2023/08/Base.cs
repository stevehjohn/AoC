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

    protected int WalkMap(string startNode, bool ghostMap = false)
    {
        var steps = 0;

        var step = 0;

        var node = startNode;

        while (true)
        {
            steps++;
            
            node = _steps[step] == 'L' ? Map[node].Left : Map[node].Right;

            if (ghostMap)
            {
                if (node.EndsWith('Z'))
                {
                    break;
                }
            }
            else
            {
                if (node == "ZZZ")
                {
                    break;
                }
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