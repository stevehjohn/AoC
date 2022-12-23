namespace AoC.Solutions.Solutions._2022._23;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = RunSimulation(true);

        return result.ToString();
    }
}