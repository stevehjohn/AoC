using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var seed = Input[0];

        var data = GenerateData(seed, 272);

        return GetChecksum(data);
    }
}