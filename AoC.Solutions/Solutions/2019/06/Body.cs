namespace AoC.Solutions.Solutions._2019._06;

public class Body
{
    public string Name { get; set; }

    public Body Orbits { get; set; }

    public List<Body> Orbiters { get; }

    public Body(string name)
    {
        Orbiters = new List<Body>();

        Name = name;
    }
}