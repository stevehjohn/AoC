namespace AoC.Solutions.Solutions._2019._18;

public class Destination
{
    public char Name { get; }

    public List<Destination> Requires { get; }

    public Destination(char name)
    {
        Name = name;

        Requires = new List<Destination>();
    }
}