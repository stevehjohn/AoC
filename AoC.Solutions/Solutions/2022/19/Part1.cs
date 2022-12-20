namespace AoC.Solutions.Solutions._2022._19;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var answer = Simulate(24);

        return answer.ToString();;
    }
}