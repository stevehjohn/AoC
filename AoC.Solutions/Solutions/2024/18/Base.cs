using System.Runtime.CompilerServices;
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._18;

public abstract class Base : Solution
{
    public override string Description => "RAM run";

    protected readonly char[,] Map = new char[Size, Size];

    private const int Size = 73;

    private readonly PriorityQueue<State, int> _queue = new();

    private readonly bool[] _visited = new bool[Size * Size];

    protected State WalkMaze()
    {
        _queue.Enqueue(new State(new Point2D(1, 1), 0, null, _visited), 0);
        
        Array.Fill(_visited, false);

        State node = null;
        
        while (_queue.Count > 0)
        {
            node = _queue.Dequeue();

            var point = node.Position.X + node.Position.Y * Size;
            
            if (_visited[point])
            {
                continue;
            }

            _visited[point] = true;
            
            if (node.Position is { X: Size - 2, Y: Size - 2 })
            {
                return node;
            }

            EnqueueMove(node, Point2D.North);

            EnqueueMove(node, Point2D.East);

            EnqueueMove(node, Point2D.South);

            EnqueueMove(node, Point2D.West);
        }

        return new State(default, -1, node?.Previous, _visited);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnqueueMove(State state, Point2D direction)
    {
        var position = state.Position + direction;

        if (Map[position.X, position.Y] != '#')
        {
            _queue.Enqueue(new State(position, state.Steps + 1, state, _visited), state.Steps * 10 + ((direction.X + 1) << 2) + direction.Y + 1);
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