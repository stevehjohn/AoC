namespace AoC.Solutions.Solutions._2022._08;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ProcessInput();

        var result = GetVisibleCount();

        return result.ToString();
    }
}