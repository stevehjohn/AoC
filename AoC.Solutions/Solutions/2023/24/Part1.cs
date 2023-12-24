using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<(LongPoint Position, LongPoint Velocity)> _hail = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        var collisions = CountCollisions(7, 27);
        
        return "Unknown";
    }

    private int CountCollisions(long min, long max)
    {
        var count = 0;

        for (var left = 0; left < _hail.Count - 1; left++)
        {
            for (var right = left + 1; right < _hail.Count; right++)
            {
            }
        }

        return count;
    }

    private bool CollidesWithin(long min, long max, LongPoint left, LongPoint right)
    {
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split('@', StringSplitOptions.TrimEntries);

            _hail.Add((LongPoint.Parse(parts[0]), LongPoint.Parse(parts[1])));
        }
    }
}