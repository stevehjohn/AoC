namespace AoC.Solutions.Solutions._2022._22;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        WalkPath();

        return GetSolution().ToString();
    }
}