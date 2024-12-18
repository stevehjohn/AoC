namespace AoC.Solutions.Solutions._2024._16;

public readonly struct Vertex
{
    public Point Direction { get; }
    
    public int Distance { get; }
    
    public Edge Edge { get; }

    public Vertex(Point direction, int distance, Edge edge)
    {
        Direction = direction;
        
        Distance = distance;

        Edge = edge;
    }
}