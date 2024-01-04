using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        WalkPath();

        return GetSolution().ToString();
    }
}