using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        return FindMarker(4).ToString();
    }
}