namespace AoC.Solutions.Solutions._2021._12;

public class Node
{
    public string Name { get; }

    public List<Node> Connections { get; }

    public bool IsBig => char.IsUpper(Name[0]);

    public bool IsStart => Name.Equals("start", StringComparison.InvariantCultureIgnoreCase);

    public bool IsEnd => Name.Equals("end", StringComparison.InvariantCultureIgnoreCase);

    public int Id { get; }

    private static int _id;

    public Node(string name)
    {
        Name = name;

        Connections = new List<Node>();

        Id = _id;

        _id++;
    }
}