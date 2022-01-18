using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var builder = new StringBuilder();

        var total = 0;

        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                total += builder.ToString().Distinct().Count();

                builder.Clear();
            }

            builder.Append(line);
        }

        total += builder.ToString().Distinct().Count();

        return total.ToString();
    }
}