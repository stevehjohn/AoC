using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var ranges = Input.Select(l => l.Split('-')).Select(s => (Low: uint.Parse(s[0]), High: uint.Parse(s[1]))).OrderBy(r => r.Low).ToList();

        var allowed = 0u;

        for (var i = 0L; i < uint.MaxValue; i++)
        {
            var match = ranges.FirstOrDefault(r => i >= r.Low && i <= r.High);

            if (match.High == 0)
            {
                allowed++;
            }
            else
            {
                i = match.High;
            }
        }

        return allowed.ToString();
    }
}