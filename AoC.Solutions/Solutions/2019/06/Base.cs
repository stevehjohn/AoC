using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._06;

public abstract class Base : Solution
{
    public override string Description => "Orbital transfers";

    protected readonly List<Body> Nodes = [];

    protected Base()
    {
        var root = new Body("COM");

        root.Orbiters.AddRange(GetOrbiters(root));
    }

    private List<Body> GetOrbiters(Body body)
    {
        var orbiters = Input.Where(i => i.StartsWith($"{body.Name})")).Select(i => new Body(i.Split(')')[1]) { Orbits = body }).ToList();

        Nodes.AddRange(orbiters);

        orbiters.ForEach(o => o.Orbiters.AddRange(GetOrbiters(o)));

        return orbiters;
    }
}