namespace AoC.Solutions.Solutions._2018._16;

public class Operation
{
    private string _name;

    private Action<int, int, int, int[]> _operation;

    public Operation(string name, Action<int, int, int, int[]> operation)
    {
        _operation = operation;
    }
}