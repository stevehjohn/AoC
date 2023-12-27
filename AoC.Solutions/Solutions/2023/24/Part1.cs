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

    private static bool CollidesWithin(long min, long max, (LongPoint Position, LongPoint Velocity) left, (LongPoint Position, LongPoint Velocity) right)
    {
        var collision = CollidesInFutureXy(left, right);

        if (collision == null)
        {
            return false;
        }

        return collision.Value.X >= min && collision.Value.X <= max && collision.Value.Y >= min && collision.Value.Y <= max;
    }
}