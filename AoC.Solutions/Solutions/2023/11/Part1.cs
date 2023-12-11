using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        ExpandUniverse();

        var result = SumShortestPaths();
        
        return result.ToString();
    }
}