namespace AoC.Solutions.Solutions._2022._17;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        var expected = 1504093567249;

        var answer = Solve(true);

        Console.WriteLine(expected - answer);

        return answer.ToString();
    }
}