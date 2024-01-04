using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        BuildGridAndReturnExposedFaceCount();

        return CountSurfaceArea().ToString();
    }
}