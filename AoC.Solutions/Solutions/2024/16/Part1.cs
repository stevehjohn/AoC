using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var score = WalkMaze();
        
        return score.ToString();
    }
}