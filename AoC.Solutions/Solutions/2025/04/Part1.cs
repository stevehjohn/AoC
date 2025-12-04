using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;

        for (var index = 0; index < Map.Length; index++)
        {
            if (Map[index] == '.')
            {
                continue;
            }

            if (Map.CountAdjacentCells(index, '@') < 4)
            {
                sum++;
            }
        }

        return sum.ToString();
    }
}