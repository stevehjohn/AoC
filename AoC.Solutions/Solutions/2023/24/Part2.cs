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
        const int area = 600;

        for (var x = -area; x < area + 1; x++)
        {
            for (var y = -area; y < area + 1; y++)
            {
                var velocity = new LongPoint(x, y, 0);
                
                var h1 = _hail[0] with { Velocity = _hail[0].Velocity - velocity };
                var h2 = _hail[1] with { Velocity = _hail[1].Velocity - velocity };
                var h3 = _hail[2] with { Velocity = _hail[2].Velocity - velocity };
                var h4 = _hail[3] with { Velocity = _hail[3].Velocity - velocity };

                var c1 = CollidesInFutureXy(h1, h2);
                var c2 = CollidesInFutureXy(h1, h3);
                var c3 = CollidesInFutureXy(h1, h4);

                if (c1 == null || c2 == null || c3 == null)
                {
                    continue;
                }

                for (var z = -area; z < area + 1; z++)
                {
                    var z1 = h1.Position.Z + h1.Velocity.Z * c1.Value.Time;
                    var z2 = h2.Position.Z + h2.Velocity.Z * c2.Value.Time;

                    if (z1 != z2)
                    {
                        continue;
                    }

                    var z3 = h3.Position.Z + h3.Velocity.Z * c3.Value.Time;

                    if (z1 != z3)
                    {
                        continue;
                    }

                    return c1.Value.X + c1.Value.Y + z1;
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

        var time = (long) Math.Abs(cx - left.Position.X) / Math.Abs(left.Velocity.X);
        
        return ((long) cx, (long) cy, time);
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