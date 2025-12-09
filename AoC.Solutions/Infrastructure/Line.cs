namespace AoC.Solutions.Infrastructure;

public readonly record struct Line(Coordinate Start, Coordinate End)
{
    public readonly long MinX = Math.Min(Start.X, End.X);
    
    public readonly long MaxX = Math.Max(Start.X, End.X);
    
    public readonly long MinY = Math.Min(Start.Y, End.Y);
    
    public readonly long MaxY = Math.Max(Start.Y, End.Y);
}
