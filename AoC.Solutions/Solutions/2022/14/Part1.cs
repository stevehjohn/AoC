namespace AoC.Solutions.Solutions._2022._14;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        CreateCave();

        var result = SimulateSand();

        return result.ToString();
    }
}