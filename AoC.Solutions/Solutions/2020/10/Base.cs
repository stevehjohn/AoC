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
}