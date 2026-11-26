using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._02;

public abstract class Base : Solution
{
    public override string Description => "Gift shop";

    protected long IterateInput(bool part2)
    {
        var sum = 0L;

        var seen = new HashSet<long>();

        foreach (var range in Input[0].Split(','))
        {
            var ids = range.Split('-');

            var start = long.Parse(ids[0]);

            var end = long.Parse(ids[1]);

            var maxDigits = ids[1].Length;

            seen.Clear();

            for (var digits = 1; digits <= maxDigits / 2; digits++)
            {
                var radix = 1L;

                for (var i = 0; i < digits; i++)
                {
                    radix *= 10;
                }

                var minBlock = radix / 10;

                var maxBlock = radix - 1;

                var factor = (Int128) radix + 1;

                var maxRepeats = part2 ? maxDigits / digits : 2;

                for (var repeats = 2; repeats <= maxRepeats; repeats++)
                {
                    var low = (long) Int128.Max(
                        minBlock, (start + factor - 1) / factor);

                    var high = (long) Int128.Min(
                        maxBlock, end / factor);

                    for (var block = low; block <= high; block++)
                    {
                        var id = (long) (block * factor);

                        if (seen.Add(id))
                        {
                            sum += id;
                        }
                    }

                    factor = factor * radix + 1;
                }
            }
        }

        return sum;
    }
}