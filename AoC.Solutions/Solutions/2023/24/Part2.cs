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

        const int stones = 5;
        
        for (var x = -area; x < area + 1; x++)
        {
            for (var y = -area; y < area + 1; y++)
            {
                var velocity = new LongPoint(x, y, 0);
                
                var h1 = _hail[0] with { Velocity = _hail[0].Velocity - velocity };
                var h2 = _hail[1] with { Velocity = _hail[1].Velocity - velocity };
                var h3 = _hail[2] with { Velocity = _hail[2].Velocity - velocity };
                var h4 = _hail[^2] with { Velocity = _hail[^2].Velocity - velocity };
                var h5 = _hail[^1] with { Velocity = _hail[^1].Velocity - velocity };

                var c1 = CollidesInFutureXy(h1, h2);
                var c2 = CollidesInFutureXy(h1, h3);
                var c3 = CollidesInFutureXy(h1, h4);
                var c4 = CollidesInFutureXy(h1, h5);

                if (c1 == null || c2 == null || c3 == null || c4 == null)
                {
                    continue;
                }

                if (c1.Value.Time != c2.Value.Time && c1.Value.Time != c3.Value.Time && c1.Value.Time != c4.Value.Time)
                {
                    continue;
                }

                Console.WriteLine(c1.Value.Time);
                
                for (var z = -area; z < area + 1; z++)
                {
                    var z1 = h1.Position.Z + (h1.Velocity.Z - z) * c1.Value.Time;
                    var z2 = h2.Position.Z + (h2.Velocity.Z - z) * c2.Value.Time;

                    if (EqualsWithinTolerance(z1, z2))
                    {
                        continue;
                    }

                    var z3 = h3.Position.Z + (h3.Velocity.Z - z) * c3.Value.Time;

                    if (EqualsWithinTolerance(z1, z3))
                    {
                        continue;
                    }

                    var z4 = h4.Position.Z + (h4.Velocity.Z - z) * c4.Value.Time;

                    if (EqualsWithinTolerance(z1, z4))
                    {
                        continue;
                    }

                    // Console.WriteLine($"{x}, {y}, {z}");
                    // Console.WriteLine($"{c1.Value.X}, {c1.Value.Y}, {z1}");
                    // Console.WriteLine((long) (c1.Value.X + c1.Value.Y + z1));

                    var result = c1.Value.X + c1.Value.Y + z1;

                    //if (result == 47 || result == 606772018765659)
                    {
                        Console.WriteLine($"{result} {c1.Value.X}, {c1.Value.Y}, {z1} {x}, {y}, {z}: Bingo!");
                    }

                    //return (long) (c1.Value.X + c1.Value.Y + z1);
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