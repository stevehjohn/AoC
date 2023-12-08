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
}