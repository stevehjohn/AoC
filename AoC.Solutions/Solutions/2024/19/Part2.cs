using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        var result = CheckEachTowel();
        
        return result.ToString();
    }
}