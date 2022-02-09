using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2017._22;

public class Cell
{
    public Point Position { get; }

    public State State { get; } = State.Infected;

    public Cell(Point position)
    {
        Position = position;
    }
}