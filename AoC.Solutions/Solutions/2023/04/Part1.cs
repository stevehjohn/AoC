using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var answer = GetAllPoints().Select(m => (int) Math.Pow(2, m - 1)).Sum();
        
        return answer.ToString();
    }
}