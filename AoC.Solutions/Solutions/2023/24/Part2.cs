using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        var result = Solve();

        return result.ToString();
    }

    private long Solve()
    {
        const int area = 270;

        const int stoneCount = 3;

        var stones = new (LongPoint Position, LongPoint Velocity)[stoneCount];

        var collisions = new (long X, long Y, double Time)[stoneCount - 1];

        (long X, long Y, double Time) firstCollision = (0, 0, 0);
        
        for (var x = 0; x < area + 1; x++)
        {
            for (var y = 0; y < area + 1; y++)
            {
                var velocity = new LongPoint(x, y, 0);

                for (var i = 0; i < stoneCount; i++)
                {
                    stones[i] = Hail[i] with { Velocity = Hail[i].Velocity - velocity }; 
                }

                var pass = true;
                
                for (var i = 1; i < stoneCount; i++)
                {
                    var collision = CollidesInFutureXy(stones[i], stones[0]);
                    
                    if (collision == null)
                    {
                        pass = false;

                        break;
                    }

                    collisions[i - 1] = collision.Value;

                    if (i == 1)
                    {
                        firstCollision = collision.Value;
                        
                        continue;
                    }

                    if (firstCollision.X != collision.Value.X || firstCollision.Y != collision.Value.Y)
                    {
                        pass = false;
                        
                        break;
                    }
                }

                if (! pass)
                {
                    continue;
                }

                for (var z = -0; z < area + 1; z++)
                {
                    var pZ = stones[1].Position.Z + (stones[1].Velocity.Z - z) * collisions[0].Time;

                    pass = true;
                    
                    for (var i = 2; i < stoneCount; i++)
                    {
                        var pZ2 = stones[i].Position.Z + (stones[i].Velocity.Z - z) * collisions[i - 1].Time;

                        // ReSharper disable once CompareOfFloatsByEqualityOperator
                        if (pZ2 != pZ)
                        {
                            pass = false;
                        }
                    }

                    if (! pass)
                    {
                        continue;
                    }
                    
                    var result = firstCollision.X + firstCollision.Y + pZ;
                    
                    return (long) result;
                }
            }
        }

        return 0;
    }
}