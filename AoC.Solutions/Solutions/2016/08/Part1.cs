using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._08;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        Solve();

        return CountLitPixels().ToString();
    }

    private int CountLitPixels()
    {
        var count = 0;

        for (var y = 0; y < 6; y++)
        {
            for (var x = 0; x < 50; x++)
            {
                count += Screen[x, y] ? 1 : 0;
            }
        }

        return count;
    }
}