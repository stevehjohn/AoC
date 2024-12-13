using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._13;

public class Machine
{
    public Point ButtonA { get; private set; }
    
    public Point ButtonB { get; private set; }
    
    public Point Target { get; private set; }

    public Machine(Point buttonA, Point buttonB, Point target)
    {
        ButtonA = buttonA;
        
        ButtonB = buttonB;
        
        Target = target;
    }
}