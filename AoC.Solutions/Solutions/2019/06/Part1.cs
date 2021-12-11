using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._06;

[UsedImplicitly]
public class Part1 : Solution
{
    private Body _root;

    private readonly List<Body> _nodes = new();

    public override string GetAnswer()
    {
        _root = new Body("COM");

        _root.Orbiters.AddRange(GetOrbiters(_root));

        var orbitCount = 0;

        foreach (var leaf in _nodes)
        {
            var node = leaf.Orbits;

            var nodesToCom = 0;

            while (node != null)
            {
                nodesToCom++;

                node = node.Orbits;
            }

            orbitCount += nodesToCom;
        }

        return orbitCount.ToString();
    }

    private List<Body> GetOrbiters(Body body)
    {
        var orbiters = Input.Where(i => i.StartsWith($"{body.Name})")).Select(i => new Body(i.Split(')')[1]) { Orbits = body }).ToList();

        _nodes.AddRange(orbiters);

        orbiters.ForEach(o => o.Orbiters.AddRange(GetOrbiters(o)));

        return orbiters;
    }
}