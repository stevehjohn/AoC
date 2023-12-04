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
            var points = 0;
            
            for (var p = 0; p < totalPoints[i]; p++)
            {
                points = points == 0 ? 1 : points * 2;
            }

            answer += points;
        }

        return answer.ToString();
    }
}