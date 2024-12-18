using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        var result = WalkMaze();
        
        return result.ToString();
    }
}