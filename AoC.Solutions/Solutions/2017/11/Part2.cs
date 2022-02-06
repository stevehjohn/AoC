namespace AoC.Solutions.Solutions._2017._11;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        var furthest = WalkPath(Input[0]).Furthest;

        return furthest.ToString();
    }
}