using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._15;

public class Node
{
    public Point Position { get; }

    public byte Value { get; }

    public List<Node> Neighbors { get; }

    public Node(Point position, byte value)
    {
        Position = position;

        Value = value;

        Neighbors = new List<Node>();
    }
}