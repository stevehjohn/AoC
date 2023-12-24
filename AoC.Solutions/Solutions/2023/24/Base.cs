using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._24;

public abstract class Base : Solution
{
    public override string Description => "Never tell me the odds";

    protected readonly List<(DoublePoint Position, DoublePoint Velocity)> Hail = new();
    
    protected static (double X, double Y, long Time)? CollidesInFuture((DoublePoint Position, DoublePoint Velocity) left, (DoublePoint Position, DoublePoint Velocity) right)
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

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split('@', StringSplitOptions.TrimEntries);

            Hail.Add((DoublePoint.Parse(parts[0]), DoublePoint.Parse(parts[1])));
        }
    }
}