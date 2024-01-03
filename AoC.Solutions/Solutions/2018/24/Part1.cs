using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._24;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = Play();

        return result.Units.ToString();
    }
}