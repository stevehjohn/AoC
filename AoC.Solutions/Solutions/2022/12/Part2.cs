namespace AoC.Solutions.Solutions._2022._12;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var steps = FindShortestPath(true);

        return steps.ToString();
    }
}