namespace AoC.Solutions.Solutions._2023._24;

public class LongPoint
{
    public long X { get; private init; }

    public long Y { get; private init; }

    public long Z { get; private init; }

    private LongPoint()
    {
    }

    public LongPoint(long x, long y, long z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static LongPoint Parse(string input)
    {
        var split = input.Split(',', StringSplitOptions.TrimEntries);

        var point = new LongPoint
        {
            X = long.Parse(split[0]),
            Y = long.Parse(split[1]),
            Z = long.Parse(split[2])
        };

        return point;
    }

    public static LongPoint operator -(LongPoint left, LongPoint right)
    {
        return new LongPoint(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }

    public override string ToString()
    {
        return $"{X}, {Y}, {Z}";
    }
}