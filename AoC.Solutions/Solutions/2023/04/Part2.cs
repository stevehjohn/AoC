using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var points = GetAllPoints();

        var counts = new int[Input.Length];
        
        Array.Fill(counts, 1);

        for (var i = 0; i < points.Length; i++)
        {
            for (var j = 0; j < points[i]; j++)
            {
                counts[i + j + 1] += counts[i];
            }
        }

        var sum = 0;

        for (var i = 0; i < points.Length; i++)
        {
            sum += counts[i];
        }

        return sum.ToString();
    }
}