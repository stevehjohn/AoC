using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        BuildMaze();

        var result = Maze.Count(p => p.Distance >= 1_000);

        return result.ToString();
    }
}