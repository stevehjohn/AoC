using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var points = GetAllPoints().ToArray();

        var counts = points.Select(_ => 1).ToArray();

        for (var i = 0; i < points.Length; i++)
        {
            for (var j = 0; j < points[i]; j++)
            {
                counts[i + j + 1] += counts[i];
            }
        }

        return counts.Sum().ToString();
    }
}