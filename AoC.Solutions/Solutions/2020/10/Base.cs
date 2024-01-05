using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._10;

public abstract class Base : Solution
{
    // ReSharper disable once StringLiteralTypo
    public override string Description => "Joltage meter";

    private List<long> _data;

    protected void ParseData()
    {
        _data = [];

        foreach (var line in Input)
        {
            _data.Add(long.Parse(line));
        }
    }

    protected List<long> GetDifferences()
    {
        var ordered = _data.OrderBy(n => n).ToList();

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