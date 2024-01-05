namespace AoC.Solutions.Solutions._2019._06;

public class Body
{
    public string Name { get; }

    public Body Orbits { get; init; }

    public List<Body> Orbiters { get; }

    public Body(string name)
    {
        Orbiters = [];

        Name = name;
    }
}