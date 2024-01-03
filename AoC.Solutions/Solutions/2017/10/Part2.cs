using AoC.Solutions.Solutions._2017.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        return KnotHash.MakeHash(Input[0]);
    }
}