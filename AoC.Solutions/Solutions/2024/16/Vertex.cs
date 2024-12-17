namespace AoC.Solutions.Solutions._2024._16;

public readonly struct Vertex
{
    public Heading Heading { get; }
    
    public int Distance { get; }
    
    public Edge Edge { get; }

    public Vertex(Heading heading, int distance, Edge edge)
    {
        Heading = heading;
        
        Distance = distance;

        Edge = edge;
    }
}