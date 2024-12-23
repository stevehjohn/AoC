namespace AoC.Solutions.Solutions._2024._23;

public class Node
{
    public string Name { get; }

    public List<Node> Connections { get; } = [];

    public Node(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"{Name}: {string.Join(", ", Connections.Select(c => c.Name))}";
    }
}