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
        const int area = 3;

        const int stoneCount = 5;

        var stones = new (LongPoint Position, LongPoint Velocity)[stoneCount];

        var collisions = new (long X, long Y, long Time)[stoneCount - 1];

        (long X, long Y, long Time) firstCollision = (0, 0, 0);
        
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
                    var collision = CollidesInFutureXy(stones[0], stones[i]);
                    
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

                    if (firstCollision.Time != collision.Value.Time || firstCollision.X != collision.Value.X || firstCollision.Y != collision.Value.Y)
                    {
                        pass = false;
                        
                        break;
                    }
                }

                if (! pass)
                {
                    continue;
                }

                var collisionTime = firstCollision.Time;
                
                for (var z = 0; z < area + 1; z++)
                {
                    var pZ = stones[0].Position.Z + (stones[0].Velocity.Z - z) * collisionTime;
                    
                    Console.Write($"\n{pZ} ");

                    for (var i = 1; i < stoneCount; i++)
                    {
                        var pZ2 = stones[i].Position.Z + (stones[i].Velocity.Z - z) * collisionTime;
                        
                        Console.Write($"{pZ2} ");
                        
                        // TODO: Equality check with pZ
                    }
                    
                    Console.WriteLine();
                    
                    var result = firstCollision.X + firstCollision.Y + pZ;
                
                    if (result == 47 || result == 606772018765659)
                    {
                        Console.WriteLine($"{result}: ({firstCollision.X}, {firstCollision.Y}, {pZ}) ({x}, {y}, {z}): Bingo!");
                    }
                    else
                    {
                        Console.WriteLine($"{result}: ({firstCollision.X}, {firstCollision.Y}, {pZ}) ({x}, {y}, {z}): Nope!");
                    }
                
                    //return result;
                }
            }
        }

        return 0;
    }
    
    private static (long X, long Y, long Time)? CollidesInFutureXy((LongPoint Position, LongPoint Velocity) left, (LongPoint Position, LongPoint Velocity) right)
    {
        var a1 = left.Velocity.Y / (double) left.Velocity.X;
        var b1 = left.Position.Y - a1 * left.Position.X;
        var a2 = right.Velocity.Y / (double) right.Velocity.X;
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

        var time = Math.Abs(cx - left.Position.X) / Math.Abs(left.Velocity.X);
        
        return ((long) Math.Round(cx), (long) Math.Round(cy), (long) Math.Round(time));
    }

    private static bool EqualsWithinTolerance(double left, double right)
    {
        return Math.Abs(right - left) < .000_000_001f;
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