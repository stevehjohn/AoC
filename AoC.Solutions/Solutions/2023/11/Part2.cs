using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._11;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        ExpandUniverse(999_999);

        var result = SumShortestPaths();
        
        return result.ToString();
    }
}