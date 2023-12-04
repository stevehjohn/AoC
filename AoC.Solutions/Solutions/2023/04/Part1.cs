using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var points = GetAllPoints();

        var answer = 0;
        
        for (var i = 0; i < points.Count; i++)
        {
            answer += (int) Math.Pow(2, points[i] - 1);
        }

        return answer.ToString();
    }
}