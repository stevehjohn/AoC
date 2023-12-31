using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        InitialiseSpells();

        var result = GetManaCostToWin(true);

        return result.ToString();
    }
}