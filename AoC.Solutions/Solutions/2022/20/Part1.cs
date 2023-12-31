namespace AoC.Solutions.Solutions._2022._20;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        MixState();

        var result = Solve();

        return result.ToString();
    }
}