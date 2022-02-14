namespace AoC.Solutions.Solutions._2016._24;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        GetDistancePairs();

        var result = GetShortestPath();

        return result.ToString();
    }
}