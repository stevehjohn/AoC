using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._20;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var ranges = Input.Select(l => l.Split('-')).Select(s => (Low: uint.Parse(s[0]), High: uint.Parse(s[1]))).OrderBy(r => r.Low).ToList();

        var lowest = ranges[0].High + 1;

        var found = true;

        while (found)
        {
            found = false;

            foreach (var range in ranges)
            {
                if (lowest >= range.Low && lowest <= range.High)
                {
                    lowest = range.High + 1;

                    found = true;

                    break;
                }
            }
        }

        return lowest.ToString();
    }
}