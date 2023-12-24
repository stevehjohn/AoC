using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<(DoublePoint Position, DoublePoint Velocity)> _hail = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        var collisions = CountCollisions(200000000000000, 400000000000000);
        
        return collisions.ToString();
    }

    private int CountCollisions(long min, long max)
    {
        var count = 0;

        for (var left = 0; left < _hail.Count - 1; left++)
        {
            for (var right = left + 1; right < _hail.Count; right++)
            {
                count += CollidesWithin(min, max, _hail[left], _hail[right]) ? 1 : 0;
            }
        }

        return count;
    }

    private static bool CollidesWithin(long min, long max, (DoublePoint Position, DoublePoint Velocity) left, (DoublePoint Position, DoublePoint Velocity) right)
    {
        var a1 = left.Velocity.Y / left.Velocity.X;
        var b1 = left.Position.Y - a1 * left.Position.X;
        var a2 = right.Velocity.Y / right.Velocity.X;
        var b2 = right.Position.Y - a2 * right.Position.X;

        if (IsClose(a1, a2))
        {
            return false;
        }

        var cx = (b2 - b1) / (a1 - a2);
        var cy = cx * a1 + b1;
        
        var future = cx > left.Position.X == left.Velocity.X > 0 && cx > right.Position.X == right.Velocity.X > 0;
        
        // Console.WriteLine($"{cx >= min && cx <= max && cy >= min && cy <= max}: {left.Position.X}, {right.Position.X}: {cx}, {cy}");
        
        return cx >= min && cx <= max && cy >= min && cy <= max && future;
    }

    private static bool IsClose(double left, double right)
    {
        return Math.Abs(right - left) < .000000001f;
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split('@', StringSplitOptions.TrimEntries);

            _hail.Add((DoublePoint.Parse(parts[0]), DoublePoint.Parse(parts[1])));
        }
    }
}