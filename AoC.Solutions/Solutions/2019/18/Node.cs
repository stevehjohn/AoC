namespace AoC.Solutions.Solutions._2019._18;

public class Node
{
    public char Name { get; }

    public Dictionary<Node, int> Children { get; }

    public Node(char name)
    {
        Name = name;

        Children = new Dictionary<Node, int>();
    }
}