namespace AoC.Solutions.Solutions._2025._11;

public class Node
{
    public string Name { get; }

    public List<Node> Connections { get; } = [];

    public Node(string name)
    {
        Name = name;
    }
}