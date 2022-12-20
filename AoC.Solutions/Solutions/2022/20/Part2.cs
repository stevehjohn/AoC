namespace AoC.Solutions.Solutions._2022._20;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput(811589153);

        MixState(10);

        var result = Solve();

        return result.ToString();
    }
}