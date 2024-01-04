using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._09;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ProcessInput(10);

        return TailVisited.Count.ToString();
    }
}