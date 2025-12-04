using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;

        Map.ForAllCells((index, value) =>
        {
            if (value == '.')
            {
                return;
            }

            var surrounding = 0;

            Map.ForAdjacentCells(index, cell =>
                {
                    if (cell == '@')
                    {
                        surrounding++;
                    }
                });

            if (surrounding < 4)
            {
                sum++;
            }
        });

        return sum.ToString();
    }
}