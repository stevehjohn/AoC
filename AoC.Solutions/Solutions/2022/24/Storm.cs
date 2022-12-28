namespace AoC.Solutions.Solutions._2022._24;

public readonly struct Storm
{
    public int X { get; }

    public int Y { get; }

    public Storm(int x, int y)
    {
        X = x;
            
        Y = y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}