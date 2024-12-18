using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._18;

public class State
{
    public Point2D Position { get; }
    
    public int Steps { get; }
    
    public State Previous { get; }
    
    public bool[] Visited { get; }

    public State(Point2D position, int steps, State previous, bool[] visited)
    {
        Position = position;
        
        Steps = steps;
        
        Previous = previous;

        Visited = visited;
    }
}