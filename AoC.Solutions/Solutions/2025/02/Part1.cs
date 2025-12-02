using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._02;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var ranges = Input[0].Split(',');

        var sum = 0L;

        foreach (var range in ranges)
        {
            var ids = range.Split('-');

            var start = long.Parse(ids[0]);

            var end = long.Parse(ids[1]);

            for (var i = start; i <= end; i++)
            {
                sum += SumInvalidIPatterns(i);
            }
        }
        
        return sum.ToString();
    }

    private static long SumInvalidIPatterns(long id)
    {
        var text = id.ToString();

        if (text.Length % 2 == 1)
        {
            return 0;
        }

        var left = text[..(text.Length / 2)];

        var right = text[(text.Length / 2)..];

        if (left == right)
        {
            return id;
        }

        return 0;
    }
}