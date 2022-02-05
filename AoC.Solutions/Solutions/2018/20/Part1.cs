using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._20;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        BuildMaze();

        var furthest = Maze.MaxBy(p => p.Distance);

        var result = Maze.Where(p => p.Position.Equals(furthest.Position)).Min(p => p.Distance);

        return result.ToString();
    }
}