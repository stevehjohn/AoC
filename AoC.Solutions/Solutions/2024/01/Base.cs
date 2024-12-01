using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._01;

public abstract class Base : Solution
{
    public override string Description => "Puzzle01";

    protected List<int> _left = [];

    protected List<int> _right = [];

    protected Dictionary<int, int> _rightCounts = [];
    
    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            _left.Add(int.Parse(parts[0]));

            var right = int.Parse(parts[1]);
            
            _right.Add(right);

            if (! _rightCounts.TryAdd(right, 1))
            {
                _rightCounts[right]++;
            }
        }
        
        _left.Sort();
        
        _right.Sort();
    }
}