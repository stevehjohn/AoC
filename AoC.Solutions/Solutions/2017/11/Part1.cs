namespace AoC.Solutions.Solutions._2017._11;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        var childPosition = WalkPath(Input[0]).Position;

        return ((Math.Abs(childPosition.X) + Math.Abs(childPosition.Y) + Math.Abs(childPosition.Z)) / 2).ToString();
    }
}