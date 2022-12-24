namespace AoC.Solutions.Solutions._2022._24;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var answer = RunSimulation(4);

        return answer.ToString();
    }
}