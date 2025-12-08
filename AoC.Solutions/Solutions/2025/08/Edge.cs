using AoC.Solutions.Infrastructure;
using AoC.Solutions.Libraries;

namespace AoC.Solutions.Solutions._2025._08;

public class Edge : IComparable<Edge>
{
    private readonly double _distance;

    public Vertex A { get; }
    
    public Vertex B { get; }

    public Edge(Vertex a, Vertex b)
    {
        A = a;
        
        B = b;
        
        _distance = Measurement.GetDistance(a, b);
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
        
        return _distance.CompareTo(other._distance);
    }

    public override string ToString()
    {
        return $"{A} - {B}: {_distance}";
    }
}