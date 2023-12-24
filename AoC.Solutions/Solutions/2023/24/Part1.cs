using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var collisions = CountCollisions(200_000_000_000_000, 400_000_000_000_000);
        
        return collisions.ToString();
    }

    private int CountCollisions(long min, long max)
    {
        var count = 0;

        for (var left = 0; left < Hail.Count - 1; left++)
        {
            for (var right = left + 1; right < Hail.Count; right++)
            {
                count += CollidesWithin(min, max, Hail[left], Hail[right]) ? 1 : 0;
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
        
        return cx >= min && cx <= max && cy >= min && cy <= max && future;
    }
}