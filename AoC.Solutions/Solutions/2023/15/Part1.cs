using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var steps = ParseInput();

        var sum = 0L;

        foreach (var step in steps)
        {
            sum += Hash(step);
        }
        
        return sum.ToString();
    }
}