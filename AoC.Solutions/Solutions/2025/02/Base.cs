using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._02;

public abstract class Base : Solution
{
    public override string Description => "Gift shop";

    protected long IterateInput()
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

        return sum;
    }

    protected abstract long SumInvalidIPatterns(long id);
}