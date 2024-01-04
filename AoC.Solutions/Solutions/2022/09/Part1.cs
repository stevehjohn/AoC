using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._09;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ProcessInput();

        return TailVisited.Count.ToString();
    }
}