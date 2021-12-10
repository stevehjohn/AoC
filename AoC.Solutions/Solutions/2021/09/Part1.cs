using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._09;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var total = 0;

        for (var y = 1; y <= Height - 2; y++)
        {
            for (var x = 1; x <= Width - 2; x++)
            {
                var point = Map[x, y];

                if (point < Map[x, y - 1]
                    && point < Map[x - 1, y]
                    && point < Map[x + 1, y]
                    && point < Map[x, y + 1])
                {
                    total += point + 1;
                }
            }
        }

        return total.ToString();
    }
}