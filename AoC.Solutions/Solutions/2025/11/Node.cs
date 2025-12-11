namespace AoC.Solutions.Solutions._2025._11;

public class Node
{
    public int Id { get; }

    public List<Node> Connections { get; } = [];

    public Node(int id)
    {
        Id = id;
    }
}