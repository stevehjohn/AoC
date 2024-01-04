using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._24;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        GetDistancePairs();

        var result = GetShortestPath();

        return result.ToString();
    }
}