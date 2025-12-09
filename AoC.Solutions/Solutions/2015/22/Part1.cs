using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = GetManaCostToWin();

        return result.ToString();
    }
}