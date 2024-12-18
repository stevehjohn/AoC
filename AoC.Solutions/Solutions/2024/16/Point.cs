namespace AoC.Solutions.Solutions._2024._16;

public struct Point : IEquatable<Point>
{
    public int X { get; private set; }
    
    public int Y { get; private set; }

    public Point(int x, int y)
    {
        X = x;
        
        Y = y;
    }

    public void StepTowards(Point other)
    {
        X += other.X == X ? 0 : other.X > X ? 1 : -1;

        Y += other.Y == Y ? 0 :  other.Y > Y ? 1 : -1;
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