namespace AoC.Solutions.Solutions._2017._25;

public class State
{
    public char Id { get; }

    public Action ZeroAction { get; }

    public Action OneAction { get; }

    public State(char id, Action zeroAction, Action oneAction)
    {
        Id = id;

        ZeroAction = zeroAction;

        OneAction = oneAction;
    }
}