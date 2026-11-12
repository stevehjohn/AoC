using AoC.Solutions.Extensions;

namespace AoC.Solutions.Infrastructure;

public readonly record struct Vertex(int X, int Y, int Z)
{
    public static Vertex Parse(string input)
    {
        var parts = input.Split(',');

        return new Vertex(int.FastParse(parts[0]), int.FastParse(parts[1]), int.FastParse(parts[2]));
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}