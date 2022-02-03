using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = GetOptimalPoint();

        return result.ToString();
    }

    private int GetOptimalPoint()
    {
        return 0;
    }
}