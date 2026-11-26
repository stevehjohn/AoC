using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        const int radix = 19;

        const int window = radix * radix * radix;

        const int patterns = window * radix;

        var totals = new int[patterns];

        var visited = new int[patterns];

        var generation = 0;

        var best = 0;

        foreach (var line in Input)
        {
            generation++;

            var seed = long.Parse(line);

            var previous = (int) (seed % 10);

            var key = 0;

            for (var i = 0; i < 2_000; i++)
            {
                seed = SimulateRound(seed);

                var price = (int) (seed % 10);
                
                var delta = price - previous + 9;

                previous = price;

                key = key % window * radix + delta;

                if (i < 3 || visited[key] == generation)
                {
                    continue;
                }

                visited[key] = generation;

                totals[key] += price;

                best = Math.Max(best, totals[key]);
            }
        }

        return best.ToString();
    }
}