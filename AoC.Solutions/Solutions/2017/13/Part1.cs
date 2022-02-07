using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var firewall = ParseInput();

        var result = GetSeverity(firewall);

        return result.ToString();
    }
}