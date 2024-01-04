using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._06;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        return FindMarker(14).ToString();
    }
}