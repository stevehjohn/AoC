using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = Play(int.FastParse(Input[0]));

        return result;
    }
}