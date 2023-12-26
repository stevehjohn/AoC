namespace AoC.Solutions.Solutions._2023._23;

public class Edge
{
    public int Id { get; set; }

    public int Length { get; set; }

    public int StartX { get; set; }
    
    public int StartY { get; set; }

    public int EndX { get; set; }
    
    public int EndY { get; set; }

    public List<Edge> Connections { get; } = new();
}