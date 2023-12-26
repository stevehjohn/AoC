namespace AoC.Solutions.Solutions._2023._23;

public class Edge
{
    public int Id { get; set; }

    public int Length { get; set; }

    public int X { get; set; }
    
    public int Y { get; set; }

    public List<Edge> Connections { get; } = new();
}