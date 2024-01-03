using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var totalPoints = GetAllPoints();

        var answer = 0;
        
        for (var i = 0; i < totalPoints.Length; i++)
        {
            answer += 1 << totalPoints[i] - 1;
        }

        return answer.ToString();
    }
}