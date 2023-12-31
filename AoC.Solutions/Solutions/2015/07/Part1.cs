using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._07;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = FindOutputValue("a");

        return result.ToString();
    }
}