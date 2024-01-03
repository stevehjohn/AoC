using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._06;

[UsedImplicitly]
public class Part2 : Base
{
    private string _santaLocation;

    public override string GetAnswer()
    {
        var steve = Nodes.Single(n => n.Name == "YOU");

        _santaLocation = Nodes.Single(n => n.Name == "SAN").Name;

        var steps = Walk(steve, steve.Orbits);

        return (steps - 1).ToString();
    }

    private int? Walk(Body origin, Body node, int steps = 0)
    {
        if (node == null)
        {
            return null;
        }

        if (node.Name == _santaLocation)
        {
            return steps;
        }

        int? up = null;

        if (node.Orbits != origin)
        {
            up = Walk(node, node.Orbits, steps + 1);
        }

        if (up != null)
        {
            return up;
        }

        foreach (var body in node.Orbiters)
        {
            if (body == origin)
            {
                continue;
            }

            var down = Walk(node, body, steps + 1);

            if (down != null)
            {
                return down;
            }
        }

        return null;
    }
}