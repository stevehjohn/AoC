using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var answer = Solve(true);

        return answer.ToString();
    }
}