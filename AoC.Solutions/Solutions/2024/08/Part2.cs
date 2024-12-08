using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        LocateNodes();

        CalculateAntiNodes(true);

        return AntiNodes.Count.ToString();
    }
}