using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._19;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var startX = ParseInputAndReturnStartX();

        var result = TravelAndCollect(startX);

        return result.Letters;
    }
}