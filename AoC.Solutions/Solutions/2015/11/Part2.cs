using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._11;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = GetNextPassword(Input[0]);

        result = GetNextPassword(result);

        return result;
    }
}