using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var seed = Input[0];

        var data = GenerateData(seed, 35_651_584);

        return GetChecksum(data);
    }
}