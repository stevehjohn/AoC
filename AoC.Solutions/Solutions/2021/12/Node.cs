namespace AoC.Solutions.Solutions._2021._12;

public class Node
{
    public string Name { get; }

    public List<Node> Connections { get; }

    public bool IsBig => char.IsUpper(Name[0]);

    public bool IsStart => Name.Equals("start", StringComparison.InvariantCultureIgnoreCase);

    public bool IsEnd => Name.Equals("end", StringComparison.InvariantCultureIgnoreCase);

    public Node(string name)
    {
        Name = name;

        Connections = new List<Node>();
    }
}