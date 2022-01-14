using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public class Node
{

    public char Name { get; }

    public Point Position { get; }

    public Node(char name, Point position)
    {
        Name = name;

        Position = position;
    }
}