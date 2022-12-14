namespace AoC.Solutions.Solutions._2022._14;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        CreateCave();

        AddFloor();

        var result = SimulateSand();

        return result.ToString();
    }
}