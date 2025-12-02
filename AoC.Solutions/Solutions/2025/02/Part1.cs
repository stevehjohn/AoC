using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._02;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var ranges = Input[0].Split(',');

        var sum = 0;

        foreach (var range in ranges)
        {
            var ids = range.Split('-');

            sum += SumInvalidIPatterns(ids[0]);

            sum += SumInvalidIPatterns(ids[1]);
        }
        
        return sum.ToString();
    }

    private int SumInvalidIPatterns(string id)
    {
        return 0;
    }
}