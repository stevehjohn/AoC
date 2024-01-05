using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._09;

public abstract class Base : Solution
{
    public override string Description => "Cypher cracking";

    protected const string Part1ResultFile = "2020.09.1.result";

    protected List<long> Data;

    protected void ParseData()
    {
        Data = [];

        foreach (var line in Input)
        {
            Data.Add(long.Parse(line));
        }
    }
}