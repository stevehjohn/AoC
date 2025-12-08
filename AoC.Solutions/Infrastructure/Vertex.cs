namespace AoC.Solutions.Infrastructure;

public readonly record struct Vertex(int X, int Y, int Z)
{
    public static Vertex Parse(string input)
    {
        var parts = input.Split(',');

        return new Vertex(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}