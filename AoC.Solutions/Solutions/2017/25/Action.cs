namespace AoC.Solutions.Solutions._2017._25;

public class Action
{
    public int Write { get; }

    public int Direction { get; }

    public char NextState { get; }

    public Action(int write, int direction, char nextState)
    {
        Write = write;

        Direction = direction;

        NextState = nextState;
    }
}