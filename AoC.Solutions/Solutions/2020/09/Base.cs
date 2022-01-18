using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._09;

public abstract class Base : Solution
{
    public override string Description => "Cypher cracking";

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