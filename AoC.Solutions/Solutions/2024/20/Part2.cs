using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var baseTime = Race();

        var count = 0;
        
        return count.ToString();
    }
}