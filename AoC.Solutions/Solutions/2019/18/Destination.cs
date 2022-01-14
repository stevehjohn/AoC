namespace AoC.Solutions.Solutions._2019._18;

public class Destination
{
    public char Name { get; }

    public List<Destination> BlockedBy { get; }

    public Destination(char name)
    {
        Name = name;

        BlockedBy = new List<Destination>();
    }
}