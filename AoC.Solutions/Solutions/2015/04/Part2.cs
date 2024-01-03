using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = GetAnswer("000000");

        return result.ToString();
    }
}