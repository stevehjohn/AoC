using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._18;

public class State
{
    public Point2D Position { get; }
    
    public int Steps { get; }
    
    public State Previous { get; }

    public State(Point2D position, int steps, State previous)
    {
        Position = position;
        
        Steps = steps;
        
        Previous = previous;
    }
}