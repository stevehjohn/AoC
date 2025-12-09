namespace AoC.Solutions.Infrastructure;

public readonly record struct Coordinate(long X, long Y)
{
    public static Coordinate Parse(string input)
    {
        var parts = input.Split(',');

        return new Coordinate(long.Parse(parts[0]), long.Parse(parts[1]));
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}