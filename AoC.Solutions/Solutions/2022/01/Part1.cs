namespace AoC.Solutions.Solutions._2022._01;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        return Calories.Max().ToString();
    }
}