using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._01;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;
        
        foreach (var line in Input)
        {
            sum += GetNumber(line);
        }

        return sum.ToString();
    }
}