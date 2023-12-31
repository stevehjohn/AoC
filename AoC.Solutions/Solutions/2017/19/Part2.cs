using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var startX = ParseInputAndReturnStartX();

        var result = TravelAndCollect(startX);

        return result.Steps.ToString();
    }
}