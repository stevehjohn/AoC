using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
     
        FindVelocityFor2DCollision();
        
        return "Unknown";
    }

    private void FindVelocityFor2DCollision()
    {
        for (var x = -10000; x < 10001; x++)
        {
            for (var y = -10000; y < 10000; y++)
            {
                var intersection = IntersectionOfAllHail2D(x, y);
                
                if (intersection != null)
                {
                    Console.WriteLine($"{x}, {y}");

                    return;
                }
            }
        }
    }

    private (double X, double Y)? IntersectionOfAllHail2D(int rockVelocityX, int rockVelocityY)
    {
        (double X, double Y)? commonCollision = null;

        for (var left = 0; left < Hail.Count - 1; left++)
        {
            for (var right = left + 1; right < Hail.Count; right++)
            {
                var leftHail = (Hail[left].Position, new DoublePoint(Hail[left].Velocity.X + rockVelocityX, Hail[left].Velocity.Y + rockVelocityY));
                
                var rightHail = (Hail[right].Position, new DoublePoint(Hail[right].Velocity.X + rockVelocityX, Hail[right].Velocity.Y + rockVelocityY));

                var collision = CollidesInFuture(leftHail, rightHail);

                if (collision == null)
                {
                    return null;
                }

                if (commonCollision == null)
                {
                    commonCollision = collision;
                    
                    continue;
                }

                if (commonCollision != collision)
                {
                    return null;
                }
            }
        }

        return commonCollision;
    }
}