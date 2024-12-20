using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._20;

public class State
{
    public Point2D Position { get; }
    
    public Point2D Direction { get; }
    
    public int Steps { get; }
    
    public State Previous { get; }

    public State(Point2D position, Point2D direction, int steps, State previous = null)
    {
        Position = position;
        
        Direction = direction;
        
        Steps = steps;

        Previous = previous;
    }
}