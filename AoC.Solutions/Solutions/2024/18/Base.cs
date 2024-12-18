using System.Runtime.CompilerServices;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._18;

public abstract class Base : Solution
{
    public override string Description => "RAM run";
    
    protected readonly char[,] Map = new char[Size, Size];

    private const int Size = 73;

    private readonly PriorityQueue<(Point Position, int Steps), int> _queue = new();

    private readonly HashSet<Point> _visited = [];

    protected int WalkMaze()
    {
        _queue.Enqueue((new Point(1, 1), 0), 0);
        
        _visited.Clear();

        while (_queue.Count > 0)
        {
            var node = _queue.Dequeue();

            if (! _visited.Add(node.Position))
            {
                continue;
            }

            if (node.Position.X == Size - 2 && node.Position.Y == Size - 2)
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

        if (Map[position.X, position.Y] != '#')
        {
            _queue.Enqueue((position, steps + 1), steps + 1);
        }
    }

    protected void ParseInput()
    {
        for (var i = 0; i < Size * Size; i++)
        {
            Map[i % Size, i / Size] = '.';
        }

        for (var i = 0; i < Size; i++)
        {
            Map[i, 0] = '#';

            Map[i, Size - 1] = '#';

            Map[0, i] = '#';

            Map[Size - 1, i] = '#';
        }

        for (var i = 0; i < 1_024; i++)
        {
            if (i >= Input.Length)
            {
                break;
            }

            var parts = Input[i].Split(',');

            Map[int.Parse(parts[0]) + 1, int.Parse(parts[1]) + 1] = '#';
        }
    }
}