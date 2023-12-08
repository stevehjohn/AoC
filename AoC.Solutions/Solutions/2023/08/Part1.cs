using JetBrains.Annotations;
using TraceReloggerLib;

namespace AoC.Solutions.Solutions._2023._08;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = WalkMap("AAA");
        
        return result.ToString();
    }
}