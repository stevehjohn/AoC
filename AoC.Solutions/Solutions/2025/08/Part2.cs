using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var vertices = Input.Select(Vertex.Parse).ToArray();

        var count = vertices.Length;

        var distances = new long[count];

        var parents = new int[count];

        var visited = new bool[count];

        Array.Fill(distances, long.MaxValue);

        distances[0] = 0;

        var longest = -1L;

        var answer = 0L;

        for (var i = 0; i < count; i++)
        {
            var next = -1;

            var shortest = long.MaxValue;

            for (var j = 0; j < count; j++)
            {
                if (! visited[j] && distances[j] < shortest)
                {
                    shortest = distances[j];

                    next = j;
                }
            }

            visited[next] = true;

            if (i > 0 && shortest > longest)
            {
                longest = shortest;

                answer = (long) vertices[next].X *
                         vertices[parents[next]].X;
            }

            var a = vertices[next];

            for (var j = 0; j < count; j++)
            {
                if (visited[j])
                {
                    continue;
                }

                var b = vertices[j];

                var dx = (long) a.X - b.X;
                var dy = (long) a.Y - b.Y;
                var dz = (long) a.Z - b.Z;

                var distance = dx * dx + dy * dy + dz * dz;

                if (distance < distances[j])
                {
                    distances[j] = distance;

                    parents[j] = next;
                }
            }
        }

        return answer.ToString();
    }
}