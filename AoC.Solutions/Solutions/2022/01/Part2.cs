namespace AoC.Solutions.Solutions._2022._01;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        return Calories.OrderByDescending(c => c).Take(3).Sum().ToString();
    }
}