namespace AoC.Solutions.Solutions._2024._14;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        Simulate(100);

        var answer = CountArea(0, 0, Width / 2, Height / 2);

        answer *= CountArea(Width / 2 + 1, 0, Width / 2, Height / 2);

        answer *= CountArea(0, 4, Width / 2, Height / 2);

        answer *= CountArea(Width / 2 + 1, Height / 2 + 1, Width / 2, Height / 2);
        
        return answer.ToString();
    }
}