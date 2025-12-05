using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseRanges();
        
        Ranges.Sort();

        var i = 0;

        while (i < Ranges.Count - 1)
        {
            var left = Ranges[i];

            var right = Ranges[i + 1];

            if (left.End >= right.Start)
            {
                Ranges[i] = (left.Start, Math.Max(left.End, right.End));
                
                Ranges.RemoveAt(i + 1);
                
                continue;
            }

            i++;
        }

        var fresh = 0L;

        foreach (var range in Ranges)
        {
            fresh += range.End - range.Start + 1;
        }

        return fresh.ToString();
    }
}