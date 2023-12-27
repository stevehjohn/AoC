using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<(LongPoint Position, LongPoint Velocity)> _hail = new();

    public override string GetAnswer()
    {
        ParseInput();
        
        var result = Solve();

        return result.ToString();
    }

    private long Solve()
    {
        const int area = 300;

        const int stoneCount = 4;

        var stones = new (LongPoint Position, LongPoint Velocity)[stoneCount];

        var collisions = new (long X, long Y, double Time)[stoneCount - 1];

        (long X, long Y, double Time) firstCollision = (0, 0, 0);
        
        for (var x = -area; x < area + 1; x++)
        {
            for (var y = -area; y < area + 1; y++)
            {
                var velocity = new LongPoint(x, y, 0);

                for (var i = 0; i < stoneCount; i++)
                {
                    stones[i] = _hail[i] with { Velocity = _hail[i].Velocity - velocity }; 
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

                for (var z = -area; z < area + 1; z++)
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
    
    private static (long X, long Y, double Time)? CollidesInFutureXy((LongPoint Position, LongPoint Velocity) left, (LongPoint Position, LongPoint Velocity) right)
    {
        if (left.Velocity.X == 0 || right.Velocity.X == 0)
        {
            return null;
        }

        var a1 = left.Velocity.Y / (double) left.Velocity.X;
        var b1 = left.Position.Y - a1 * left.Position.X;
        var a2 = right.Velocity.Y / (double) right.Velocity.X;
        var b2 = right.Position.Y - a2 * right.Position.X;

        var cx = (b2 - b1) / (a1 - a2);
        
        var t1 = (cx - left.Position.X) / left.Velocity.X;
        var t2 = (cx - right.Position.X) / right.Velocity.X;

        if (t1 < 0 || t2 < 0)
        {
            return null;
        }

        var cy = a1 * (cx - left.Position.X) + left.Position.Y;
        
        return ((long) Math.Round(cx), (long) Math.Round(cy), Math.Round(t1, 3));
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