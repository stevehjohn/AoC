namespace AoC.Solutions.Solutions._2024._23;

public class Node
{
    public int Id { get; }

    public List<Node> Connections { get; } = [];

    public Node(int id)
    {
        Id = id;
    }

    public override string ToString()
    {
        return $"{Id}: {string.Join(", ", Connections.Select(c => c.Id))}";
    }
}