using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
     
        FindVelocityForCollision();
        
        return "Unknown";
    }

    private void FindVelocityForCollision()
    {
        for (var x = -100; x < 101; x++)
        {
            for (var y = -100; y < 100; y++)
            {
                var intersection = IntersectionOfHail2D(x, y);
                
                if (intersection != null)
                {
                    Console.WriteLine($"{x}, {y}, {intersection.Value.Time}");

                    var leftHail = Hail[0];

                    for (var i = 1; i < Hail.Count; i++)
                    {
                        var rightHail = Hail[i];
                    }

                    return;
                }
            }
        }
    }

    private (double X, double Y, long Time)? IntersectionOfHail2D(int rockVelocityX, int rockVelocityY)
    {
        (double X, double Y, long Time)? commonCollision = null;

        for (var right = 1; right < Hail.Count; right++)
        {
            var leftHail = (Hail[0].Position, new DoublePoint(Hail[0].Velocity.X + rockVelocityX, Hail[0].Velocity.Y + rockVelocityY));
            
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

        return commonCollision;
    }
}