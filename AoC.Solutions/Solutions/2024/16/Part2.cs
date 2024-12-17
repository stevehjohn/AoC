using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var graph = new Graph(Input.To2DArray());

        var result = graph.WalkToEnd((direction, vertex) =>
            direction == vertex.Heading ? vertex.Distance : 1_000 + vertex.Distance);

        return result.ToString();
    }
}