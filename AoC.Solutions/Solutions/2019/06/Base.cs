using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._06;

public abstract class Base : Solution
{
    public override string Description => "Orbital transfers";

    protected Body Root;

    protected readonly List<Body> Nodes = new();

    protected Base()
    {
        Root = new Body("COM");

        Root.Orbiters.AddRange(GetOrbiters(Root));
    }

    protected List<Body> GetOrbiters(Body body)
    {
        var orbiters = Input.Where(i => i.StartsWith($"{body.Name})")).Select(i => new Body(i.Split(')')[1]) { Orbits = body }).ToList();

        Nodes.AddRange(orbiters);

        orbiters.ForEach(o => o.Orbiters.AddRange(GetOrbiters(o)));

        return orbiters;
    }
}