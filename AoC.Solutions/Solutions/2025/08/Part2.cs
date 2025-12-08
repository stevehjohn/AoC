using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        CalculateDistances();

        Edges.Sort();

        var disjointSet = new DisjointSet<Vertex>();

        foreach (var v in Junctions)
        {
            disjointSet.Add(v);
        }

        var components = Junctions.Length;

        long answer = 0;

        foreach (var edge in Edges)
        {
            if (disjointSet.Union(edge.A, edge.B))
            {
                components--;

                if (components == 1)
                {
                    answer = (long) edge.A.X * edge.B.X;

                    break;
                }
            }
        }

        return answer.ToString();
    }
}