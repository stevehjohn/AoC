namespace AoC.Solutions.Common;

public class Point
{
    public int X { get; set; }

    public int Y { get; set; }

    public int Z { get; set; }

    public Point(Point point)
    {
        X = point.X;

        Y = point.Y;

        Z = point.Z;
    }

    public Point(int x, int y, int z = 0)
    {
        X = x;

        Y = y;

        Z = z;
    }

    public Point()
    {
    }

    public static Point Parse(string input)
    {
        var split = input.Split(',', StringSplitOptions.TrimEntries);

        var point = new Point
                    {
                        X = int.Parse(split[0]),
                        Y = int.Parse(split[1])
                    };

        if (split.Length > 2)
        {
            point.Z = int.Parse(split[2]);
        }

        return point;
    }

    public static Point operator +(Point left, Point right)
    {
        return new Point(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public static Point operator -(Point left, Point right)
    {
        return new Point(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }

    public override string ToString()
    {
        return $"{X,5},{Y,5},{Z,5}";
    }

    private bool Equals(Point other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Point) obj);
    }

    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode - sue me, I need Point to be mutable sometimes...
        return HashCode.Combine(X, Y, Z);
        // ReSharper restore NonReadonlyMemberInGetHashCode
    }
}