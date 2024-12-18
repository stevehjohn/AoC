namespace AoC.Solutions.Solutions._2024._16;

public readonly struct Point : IEquatable<Point>
{
    public int X { get; }
    
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        
        Y = y;
    }

    public static readonly Point North = new(0, -1);

    public static readonly Point East = new(1, 0);

    public static readonly Point South = new(0, 1);

    public static readonly Point West = new(-1, 0);

    public static readonly Point Null = new(int.MinValue, int.MaxValue);

    public static Point operator +(Point left, Point right)
    {
        return new Point(left.X + right.X, left.Y + right.Y);
    }

    public static Point operator -(Point left)
    {
        return new Point(-left.X, -left.Y);
    }
    
    public static bool operator ==(Point left, Point right)
    {
        return left.X == right.X && left.Y == right.Y;
    }

    public static bool operator !=(Point left, Point right)
    {
        return !(left == right);
    }
    
    public bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        return obj is Point other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"{X}, {Y}";
    }
}