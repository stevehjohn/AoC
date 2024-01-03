using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._10;

public abstract class Base : Solution
{
    // ReSharper disable once StringLiteralTypo
    public override string Description => "Joltage meter";

    protected List<long> Data;

    protected void ParseData()
    {
        Data = new List<long>();

        foreach (var line in Input)
        {
            Data.Add(long.Parse(line));
        }
    }

    protected List<long> GetDifferences()
    {
        var ordered = Data.OrderBy(n => n).ToList();

        var differences = new List<long>
                          {
                              1
                          };

        for (var i = 1; i < ordered.Count; i++)
        {
            differences.Add(ordered[i] - ordered[i - 1]);
        }

        differences.Add(3);

        return differences;
    }
}