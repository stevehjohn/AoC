using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._07;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly Dictionary<(int x, int y), long> _cache = new();
    
    public override string GetAnswer()
    {
        ParseInput();
        
        return Walk(Width / 2, 2).ToString();
    }

    private long Walk(int x, int y)
    {
        if (y >= Height)
        {
            return 1;
        }

        if (_cache.TryGetValue((x, y), out var cached))
        {
            return cached;
        }

        long timelines;
        
        if (Map[x, y] == '^')
        {
            timelines = Walk(x - 1, y + 2) + Walk(x + 1, y + 2);
        }
        else
        {
            timelines = Walk(x, y + 2);
        }
        
        _cache[(x, y)] = timelines;
        
        return timelines;
    }
}