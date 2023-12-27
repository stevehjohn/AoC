using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._24;

public abstract class Base : Solution
{
    public override string Description => "Never tell me the odds";
    
    protected readonly List<(LongPoint Position, LongPoint Velocity)> Hail = new();

    protected static (long X, long Y, double Time)? CollidesInFutureXy((LongPoint Position, LongPoint Velocity) left, (LongPoint Position, LongPoint Velocity) right)
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

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split('@', StringSplitOptions.TrimEntries);

            Hail.Add((LongPoint.Parse(parts[0]), LongPoint.Parse(parts[1])));
        }
    }
}