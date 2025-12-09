namespace AoC.Solutions.Infrastructure;

public readonly record struct Line
{
    public readonly long MinX;
    
    public readonly long MaxX;
    
    public readonly long MinY;
    
    public readonly long MaxY;

    public Line(Coordinate start, Coordinate end)
    {
        MinX = Math.Min(start.X, end.X);
        
        MaxX = Math.Max(start.X, end.X);
        
        MinY = Math.Min(start.Y, end.Y);
        
        MaxY = Math.Max(start.Y, end.Y);
    }
}
