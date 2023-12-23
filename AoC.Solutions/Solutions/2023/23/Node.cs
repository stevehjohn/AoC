namespace AoC.Solutions.Solutions._2023._23;

public class Node
{
    public int X { get; set; }

    public int Y { get; set; }

    public List<(Node Node, int Steps)> Connetions { get; } = new();
}