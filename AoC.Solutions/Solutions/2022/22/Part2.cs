namespace AoC.Solutions.Solutions._2022._22;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        IsCube = true;

        ParseInput();

        WalkPath();

        return GetSolution().ToString();
    }
}