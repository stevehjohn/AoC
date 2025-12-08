using AoC.Solutions.Infrastructure;
using AoC.Solutions.Libraries;

namespace AoC.Solutions.Solutions._2025._08;

public class Edge : IComparable<Edge>
{
    public Vertex A { get; }
    
    public Vertex B { get; }
    
    public double Distance { get; }

    public Edge(Vertex a, Vertex b)
    {
        A = a;
        
        B = b;
        
        Distance = Measurement.GetDistance(a, b);
    }

    public int CompareTo(Edge other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }
        
        return Distance.CompareTo(other.Distance);
    }

    public override string ToString()
    {
        return $"{A} - {B}: {Distance}";
    }
}