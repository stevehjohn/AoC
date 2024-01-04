using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        IsCube = true;

        WalkPath();

        return GetSolution().ToString();
    }
}