using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var start = ParseInput();

        var result = Solve(start, 5000);
        
        return result.ToString();
    }

    private long Solve((int X, int Y) start, int maxSteps)
    {
        return 0;
    }
}