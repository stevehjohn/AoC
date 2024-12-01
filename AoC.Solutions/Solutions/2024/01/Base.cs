using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._01;

public abstract class Base : Solution
{
    public override string Description => "Puzzle01";

    protected List<int> _left = [];

    protected List<int> _right = [];
    
    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            _left.Add(int.Parse(parts[0]));
            
            _right.Add(int.Parse(parts[1]));
        }
        
        _left.Sort();
        
        _right.Sort();
    }
}