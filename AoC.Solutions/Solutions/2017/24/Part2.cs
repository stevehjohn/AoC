using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = Solve().StrongestLongest;

        return result.ToString();
    }
}