namespace AoC.Solutions.Solutions._2022._18;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        BuildGridAndReturnExposedFaceCount();

        return CountSurfaceArea().ToString();
    }
}