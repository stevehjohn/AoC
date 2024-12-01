using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._01;

public abstract class Base : Solution
{
    public override string Description => "Historian Hysteria";

    protected readonly List<int> Left = [];

    protected readonly List<int> Right = [];

    protected readonly Dictionary<int, int> RightCounts = [];
    
    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            Left.Add(int.Parse(parts[0]));

            var right = int.Parse(parts[1]);
            
            Right.Add(right);

            if (! RightCounts.TryAdd(right, 1))
            {
                RightCounts[right]++;
            }
        }
        
        Left.Sort();
        
        Right.Sort();
    }
}