using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = Solve(true);
        
        return result.ToString();
    }
}