namespace AoC.Solutions.Solutions._2017._13;

public class Layer
{
    public int Depth { get; }

    public int Position { get; private set; }

    public int Period => (Depth - 1) * 2;

    private int _direction = 1;

    public Layer(int depth)
    {
        Depth = depth;
    }

    public void MoveScanner()
    {
        Position += _direction;

        if (Position == 0 || Position == Depth - 1)
        {
            _direction = -_direction;
        }
    }
}