using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._06;

public abstract class Base : Solution
{
    public override string Description => "Signals and noise";

    protected string GetAnswer(bool isPart2)
    {
        var columns = new List<List<char>>();

        for (var i = 0; i < Input[0].Length; i++)
        {
            columns.Add(new List<char>());
        }

        foreach (var line in Input)
        {
            for (var i = 0; i < line.Length; i++)
            {
                columns[i].Add(line[i]);
            }
        }

        var builder = new StringBuilder();

        for (var i = 0; i < columns.Count; i++)
        {
            if (isPart2)
            {
                builder.Append(columns[i].GroupBy(c => c).MinBy(c => c.Count())!.Key);
            }
            else
            {
                builder.Append(columns[i].GroupBy(c => c).MaxBy(c => c.Count())!.Key);
            }
        }

        return builder.ToString();
    }
}