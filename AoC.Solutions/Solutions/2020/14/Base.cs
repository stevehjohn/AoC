using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._14;

public abstract class Base : Solution
{
    public override string Description => "Memory bit twiddling";

    protected static (int Location, long Value) ParseInstruction(string instruction)
    {
        var split = instruction[4..].Split('=', StringSplitOptions.TrimEntries);

        var locationString = split[0];

        locationString = locationString[..locationString.IndexOf(']')];

        return (int.Parse(locationString), long.Parse(split[1]));
    }
}