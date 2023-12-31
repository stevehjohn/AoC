using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var orbitCount = 0;

        foreach (var leaf in Nodes)
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
}