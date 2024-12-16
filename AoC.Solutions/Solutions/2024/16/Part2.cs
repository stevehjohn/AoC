using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        IsPart2 = true;
        
        ParseInput();

        var score = WalkMaze();
        
        return score.ToString();
    }
}