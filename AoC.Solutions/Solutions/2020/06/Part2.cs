using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._06;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var builder = new StringBuilder();

        var total = 0;

        var lines = 0;

        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (lines == 1)
                {
                    total += builder.ToString().Distinct().Count();
                }

                builder.Clear();

                lines = 0;
            }

            builder.Append(line);

            lines++;
        }

        total += builder.ToString().Distinct().Count();

        return total.ToString();
    }
}