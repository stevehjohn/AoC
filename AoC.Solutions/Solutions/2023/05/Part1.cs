using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._05;

[UsedImplicitly]
public class Part1 : Base
{
    private List<int> _seeds;
    
    public override string GetAnswer()
    {
        return "Unknown";
    }

    private void ParseInput()
    {
        _seeds = Input[0][6..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        
        
    }
}