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
        var collision = CollidesInFutureXy(left, right);

        if (collision == null)
        {
            return false;
        }

        return collision.Value.X >= min && collision.Value.X <= max && collision.Value.Y >= min && collision.Value.Y <= max;
    }
    
    private static (double X, double Y, long Time)? CollidesInFutureXy((DoublePoint Position, DoublePoint Velocity) left, (DoublePoint Position, DoublePoint Velocity) right)
    {
        var a1 = left.Velocity.Y / left.Velocity.X;
        var b1 = left.Position.Y - a1 * left.Position.X;
        var a2 = right.Velocity.Y / right.Velocity.X;
        var b2 = right.Position.Y - a2 * right.Position.X;

        if (EqualsWithinTolerance(a1, a2))
        {
            return null;
        }

        var cx = (b2 - b1) / (a1 - a2);
        var cy = cx * a1 + b1;

        var future = cx > left.Position.X == left.Velocity.X > 0 && cx > right.Position.X == right.Velocity.X > 0;

        if (! future)
        {
            return null;
        }

        var time = (long) Math.Ceiling(Math.Abs(cx - left.Position.X) / Math.Abs(left.Velocity.X));
        
        return (cx, cy, time);
    }

    private static bool EqualsWithinTolerance(double left, double right)
    {
        return Math.Abs(right - left) < .000_000_001f;
    }
}