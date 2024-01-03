using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        Width = 32;

        Height = 40;

        BuildMaze();

        var result = Solve(31, 39);

        return result.ToString();
    }
}