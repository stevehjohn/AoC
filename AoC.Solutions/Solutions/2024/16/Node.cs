using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._16;

public class Node
{
    public Edge Edge { get; }
    
    public Point2D Direction { get; }
    
    public int Score { get; }
    
    public Node Previous { get; }

    public Node(Edge edge, Point2D direction, int score, Node previous)
    {
        Edge = edge;
        
        Direction = direction;
        
        Score = score;
        
        Previous = previous;
    }
}