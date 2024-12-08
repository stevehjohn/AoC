using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._08;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        LocateNodes();

        CalculateAntiNodes();

        return AntiNodes.Count.ToString();
    }
}