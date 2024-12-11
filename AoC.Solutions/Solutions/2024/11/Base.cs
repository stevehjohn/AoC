using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._11;

public abstract class Base : Solution
{
    public override string Description => "Plutonian pebbles";

    protected long[] Stones;

    private readonly Dictionary<string, long> _cache = [];
    
    protected void ParseInput()
    {
        Stones = Input[0].Split(' ').Select(long.Parse).ToArray();
    }

    protected long Blink(long stone, int times)
    {
        var key = $"{stone}|{times}";

        if (_cache.TryGetValue(key, out var value))
        {
            return value;
        }

        long sum;

        var digits = (int) Math.Log10(stone) + 1;

        if (times == 1)
        {
            sum = digits % 2 == 0 ? 2 : 1;
        }
        else
        {
            if (stone == 0)
            {
                sum = Blink(1, times - 1);
            }
            else if (digits % 2 == 0)
            {
                var pow = 1;
        
                for (var i = 0; i < digits / 2; i++)
                {
                    pow *= 10;
                }

                sum = Blink(stone / pow, times - 1);

                sum += Blink(stone % pow, times - 1);
            }
            else
            {
                sum = Blink(stone * 2_024, times - 1);
            }
        }
        
        _cache.Add(key, sum);

        return sum;
    }
}