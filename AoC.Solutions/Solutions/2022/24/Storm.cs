namespace AoC.Solutions.Solutions._2022._24;

public struct Storm
{
    public char Direction { get; }

    public int X { get; }

    public int Y { get; }

    public Storm(char direction, int x, int y)
    {
        Direction = direction;

        X = x;
            
        Y = y;
    }

    public override string ToString()
    {
        return $"{Direction} ({X}, {Y})";
    }
}