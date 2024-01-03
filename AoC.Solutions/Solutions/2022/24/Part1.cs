namespace AoC.Solutions.Solutions._2022._24;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var answer = RunSimulation();

        return answer.ToString();
    }
}