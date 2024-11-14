using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._02;

public abstract class Base : Solution
{
    public override string Description => "Password policy";

    protected static (int A, int B, char Character, string Password) ParseLine(string line)
    {
        var split = line.Split(':', StringSplitOptions.TrimEntries);

        var password = split[1];

        split = split[0].Split(' ');

        var character = split[1][0];

        split = split[0].Split('-');

        return (A: int.Parse(split[0]), B: int.Parse(split[1]), Character: character, Password: password);
    }
}