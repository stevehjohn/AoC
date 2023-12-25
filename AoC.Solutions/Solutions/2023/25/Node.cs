namespace AoC.Solutions.Solutions._2023._25;

public class Node
{
    public string Name { get; set; }

    public List<Node> Connections { get; } = new();
}