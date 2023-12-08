using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var steps = 0;

        var starts = Map.Where(m => m.Key.EndsWith('A')).Select(m => m.Key).ToList();

        foreach (var start in starts)
        {
            steps += WalkMap(start, true);
        }
        
        return steps.ToString();
    }
}