using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var best = new int[Size];

        var current = new int[Size];

        var bestCount = 0;

        Span<int> candidates = stackalloc int[Size];

        for (var node = 0; node < Size; node++)
        {
            var neighbours = Neighbours[node];

            if (neighbours.Length == 0 ||
                neighbours.Length + 1 <= bestCount)
            {
                continue;
            }

            var count = 0;

            foreach (var neighbour in neighbours)
            {
                if (neighbour > node)
                {
                    candidates[count++] = neighbour;
                }
            }

            current[0] = node;

            Search(candidates[..count], 1);
        }

        var names = new string[bestCount];

        for (var i = 0; i < bestCount; i++)
        {
            var id = best[i];

            names[i] = string.Create(2, id,
                static (span, value) =>
                {
                    span[0] = (char) ('a' + value / 26);

                    span[1] = (char) ('a' + value % 26);
                });
        }

        return string.Join(',', names);

        void Search(ReadOnlySpan<int> available, int depth)
        {
            if (depth > bestCount)
            {
                bestCount = depth;

                Array.Copy(current, best, depth);
            }

            if (depth + available.Length <= bestCount)
            {
                return;
            }

            Span<int> next = stackalloc int[available.Length];

            for (var i = 0; i < available.Length; i++)
            {
                if (depth + available.Length - i <= bestCount)
                {
                    break;
                }

                var node = available[i];

                var count = 0;

                var offset = node * Size;

                for (var j = i + 1; j < available.Length; j++)
                {
                    var candidate = available[j];

                    if (Adjacent[offset + candidate])
                    {
                        next[count++] = candidate;
                    }
                }

                current[depth] = node;

                Search(next[..count], depth + 1);
            }
        }
    }
}