namespace AoC.Solutions.Solutions._2017._12;

public class Node
{
    public int Id { get; }

    public HashSet<Node> Connections { get; } = [];

    public Node(int id)
    {
        Id = id;
    }
}