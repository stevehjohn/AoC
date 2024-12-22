using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var visited = new HashSet<long>();

        var patternResults = new Dictionary<long, int>();
        
        foreach (var line in Input)
        {
            var sequence = GetSequence(long.Parse(line));
            
            visited.Clear();

            for (var i = 0; i < sequence.Count - 4; i++)
            {
                var key = (sequence[i].Delta << 24) | (sequence[i + 1].Delta << 16) | (sequence[i + 2].Delta << 8) | sequence[i + 3].Delta;

                if (visited.Add(key))
                {
                    if (! patternResults.TryAdd(key, sequence[i + 3].Price))
                    {
                        patternResults[key] += sequence[i + 3].Price;
                    }
                }
            }
        }

        return patternResults.MaxBy(p => p.Value).Value.ToString();
    }

    private static List<(byte Price, byte Delta)> GetSequence(long seed)
    {
        var previous = (byte) (seed % 10);
        
        var sequence = new List<(byte Price, byte Delta)>();

        for (var i = 0; i < 1_999; i++)
        {
            seed = SimulateRound(seed);

            var price = (byte) (seed % 10);
            
            sequence.Add((price, (byte) (price - previous)));

            previous = price;
        }

        return sequence;
    }
}