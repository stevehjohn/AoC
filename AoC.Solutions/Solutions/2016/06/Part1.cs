using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
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
            builder.Append(columns[i].GroupBy(c => c).MaxBy(c => c.Count())!.Key);
        }

        return builder.ToString();
    }
}