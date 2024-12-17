namespace AoC.Solutions.Solutions._2024._16;

public readonly struct Vertex
{
    public Point Heading { get; }
    
    public int Distance { get; }
    
    public Edge Edge { get; }

    public Vertex(Point heading, int distance, Edge edge)
    {
        Heading = heading;
        
        Distance = distance;

        Edge = edge;
    }
}