namespace AoC.Solutions.Solutions._2024._14;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        Simulate(100);

        var answer = CountArea(0, 0, 5, 3);

        answer *= CountArea(6, 0, 5, 3);

        answer *= CountArea(0, 4, 5, 3);

        answer *= CountArea(6, 4, 5, 3);
        
        return answer.ToString();
    }
}