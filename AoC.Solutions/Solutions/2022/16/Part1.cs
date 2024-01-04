using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        OptimiseGraph();

        Traverse(30);

        return StateCache.Max(s => s.Value).Flow.ToString();
    }
}