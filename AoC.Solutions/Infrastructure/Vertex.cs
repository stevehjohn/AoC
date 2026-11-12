using AoC.Solutions.Extensions;

namespace AoC.Solutions.Infrastructure;

public readonly record struct Vertex(int X, int Y, int Z)
{
    public static Vertex Parse(string input)
    {
        var parts = input.Split(',');

        return new Vertex(parts[0].ParseToInt(), parts[1].ParseToInt(), parts[2].ParseToInt());
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}