namespace AoC.Solutions.Solutions._2022._09;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ProcessInput();

        return TailVisited.Count.ToString();
    }
}