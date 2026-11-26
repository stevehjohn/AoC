using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var steps = int.FastParse(Input[0]);

        const int limit = 50_000_000;

        var position = 0;
        
        var size = 1;
        
        var target = 0;

        while (size <= limit)
        {
            var skip = (size - position - 1) / steps;

            if (skip > 0)
            {
                skip = Math.Min(skip, limit - size + 1);

                position += skip * (steps + 1);
                
                size += skip;

                continue;
            }

            position = (position + steps) % size + 1;

            if (position == 1)
            {
                target = size;
            }

            size++;
        }

        return target.ToString();
    }
}