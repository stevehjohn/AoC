using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public class Node
{

    public char Name { get; }

    public Point Position { get; }

    public Dictionary<char, int> Distances { get; }

    public Node(char name, Point position)
    {
        Name = name;

        Position = position;

        Distances = new Dictionary<char, int>();
    }
}