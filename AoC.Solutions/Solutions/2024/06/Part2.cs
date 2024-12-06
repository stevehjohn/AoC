using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._06;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = WalkMap();
        
        return result.ToString();
    }
}