using System.Runtime.CompilerServices;
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._18;

public abstract class Base : Solution
{
    public override string Description => "RAM run";

    protected readonly char[,] Map = new char[Size, Size];

    private const int Size = 73;

    private readonly PriorityQueue<(Point2D Position, int Steps), int> _queue = new();

    private readonly bool[] _visited = new bool[Size * Size];

    protected int WalkMaze()
    {
        _queue.Enqueue((new Point2D(1, 1), 0), 0);
        
        Array.Fill(_visited, false);

        while (_queue.Count > 0)
        {
            var node = _queue.Dequeue();

            var point = node.Position.X + node.Position.Y * Size;
            
            if (_visited[point])
            {
                continue;
            }

            _visited[point] = true;
            
            if (node.Position is { X: Size - 2, Y: Size - 2 })
            {
                return node.Steps;
            }

            EnqueueMove(node.Position, Point2D.North, node.Steps);

            EnqueueMove(node.Position, Point2D.East, node.Steps);

            EnqueueMove(node.Position, Point2D.South, node.Steps);

            EnqueueMove(node.Position, Point2D.West, node.Steps);
        }

        return -1;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnqueueMove(Point2D position, Point2D direction, int steps)
    {
        position += direction;

        if (Map[position.X, position.Y] != '#')
        {
            _queue.Enqueue((position, steps + 1), steps + 1);
        }
    }

    protected void ParseInput(int maxBytes)
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

        for (var i = 0; i < maxBytes; i++)
        {
            if (i >= Input.Length)
            {
                break;
            }

            var position = new Point2D(Input[i]);

            Map[position.X + 1, position.Y + 1] = '#';
        }
    }
}