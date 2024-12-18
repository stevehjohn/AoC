using System.Runtime.CompilerServices;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._18;

public abstract class Base : Solution
{
    public override string Description => "RAM run";

    private readonly char[,] _map = new char[Size, Size];

    private const int Size = 73;

    private readonly PriorityQueue<(Point Position, int Steps), int> _queue = new();

    private readonly HashSet<Point> _visited = [];

    protected int WalkMaze(Point newByte = default)
    {
        if (newByte != default)
        {
            _map[newByte.X, newByte.Y] = '#';
        }

        _queue.Enqueue((new Point(1, 1), 0), 0);
        
        _visited.Clear();

        while (_queue.Count > 0)
        {
            var node = _queue.Dequeue();

            if (! _visited.Add(node.Position))
            {
                continue;
            }
            
            if (node.Position is { X: Size - 2, Y: Size - 2 })
            {
                return node.Steps;
            }

            EnqueueMove(node.Position, Point.North, node.Steps);

            EnqueueMove(node.Position, Point.East, node.Steps);

            EnqueueMove(node.Position, Point.South, node.Steps);

            EnqueueMove(node.Position, Point.West, node.Steps);
        }

        return -1;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnqueueMove(Point position, Point direction, int steps)
    {
        position += direction;

        if (_map[position.X, position.Y] != '#')
        {
            _queue.Enqueue((position, steps + 1), steps + 1);
        }
    }

    protected void ParseInput()
    {
        for (var i = 0; i < Size * Size; i++)
        {
            _map[i % Size, i / Size] = '.';
        }

        for (var i = 0; i < Size; i++)
        {
            _map[i, 0] = '#';

            _map[i, Size - 1] = '#';

            _map[0, i] = '#';

            _map[Size - 1, i] = '#';
        }

        for (var i = 0; i < 1_024; i++)
        {
            if (i >= Input.Length)
            {
                break;
            }

            var position = new Point(Input[i]);

            _map[position.X + 1, position.Y + 1] = '#';
        }
    }
}