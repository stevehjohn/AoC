using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._02;

[UsedImplicitly]
public class Part2 : Base
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

        for (var patternLength = 1; patternLength <= text.Length / 2; patternLength++)
        {
            if (text.Length % patternLength != 0)
            {
                continue;
            }

            var pattern = text[..patternLength];
            
            var isRepeating = true;

            for (var k = patternLength; k < text.Length; k += patternLength)
            {
                var segment = text[k..(k + patternLength)];
            
                if (segment != pattern)
                {
                    isRepeating = false;
                    
                    break;
                }
            }

            if (isRepeating)
            {
                return id;
            }
        }

        return 0;
    }
}