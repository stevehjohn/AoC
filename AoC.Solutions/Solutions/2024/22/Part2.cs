using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._22;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly (byte Price, byte Delta)[] _sequence = new(byte Price, byte Delta)[2_000];
    
    public override string GetAnswer()
    {
        var visited = new HashSet<long>();

        var patternResults = new Dictionary<long, int>();
        
        foreach (var line in Input)
        {
            GetSequence(long.Parse(line));
            
            visited.Clear();

            for (var i = 0; i < _sequence.Length - 4; i++)
            {
                var key = (_sequence[i].Delta << 24) | (_sequence[i + 1].Delta << 16) | (_sequence[i + 2].Delta << 8) | _sequence[i + 3].Delta;

                if (visited.Add(key))
                {
                    if (! patternResults.TryAdd(key, _sequence[i + 3].Price))
                    {
                        patternResults[key] += _sequence[i + 3].Price;
                    }
                }
            }
        }

        return patternResults.MaxBy(p => p.Value).Value.ToString();
    }

    private void GetSequence(long seed)
    {
        var previous = (byte) (seed % 10);
        
        for (var i = 0; i < 1_999; i++)
        {
            seed = SimulateRound(seed);

            var price = (byte) (seed % 10);
            
            _sequence[i] = (price, (byte) (price - previous));

            previous = price;
        }
    }
}