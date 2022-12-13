using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._12;

public abstract class Base : Solution
{
    public override string Description => "Hill climbing algorithm";

    private int _width;

    private int _height;

    private byte[,] _map;

    private Point _start;

    private Point _end;

    private readonly HashSet<Point> _visited = new();

    private readonly PriorityQueue<(Point Position, int Steps), int> _queue = new();

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new byte[_width, _height];

        for (var y = 0; y < Input.Length; y++)
        {
            for (var x = 0; x < Input[y].Length; x++)
            {
                var character = Input[y][x];

                if (character == 'S')
                {
                    _start = new Point(x, y);

                    _map[x, y] = 0;

                    continue;
                }

                if (character == 'E')
                {
                    _end = new Point(x, y);

                    _map[x, y] = 'z' - 'a';

                    continue;
                }

                _map[x, y] = (byte) (character - 'a');
            }
        }
    }

    protected int FindShortestPath(bool startFromEnd = false)
    {
        if (startFromEnd)
        {
            _queue.Enqueue((_end, 0), 0);
        }
        else
        {
            _queue.Enqueue((_start, 0), 0);
        }
        
        Func<byte, byte, bool> comparer;

        if (startFromEnd)
        {
            comparer = (l, h) => l >= h - 1;
        }
        else
        {
            comparer = (l, h) => l <= h + 1;
        }

        while (_queue.Count > 0)
        {
            var node = _queue.Dequeue();

            var position = node.Position;

            if (startFromEnd)
            {
                if (_map[position.X, position.Y] == 0)
                {
                    return node.Steps;
                }
            }
            else
            {
                if (position.Equals(_end))
                {
                    return node.Steps;
                }
            }

            var height = _map[position.X, position.Y];

            var manhattan = 0;

            if (! startFromEnd)
            {
                manhattan = Math.Abs(position.X - _end.X) + Math.Abs(position.Y - _end.Y);
            }

            AddPossibleMoves(position, comparer, height, node.Steps, manhattan);
        }

        return int.MaxValue;
    }

    private void AddPossibleMoves(Point position, Func<byte, byte, bool> comparer, byte height, int steps, int manhattan)
    {
        Point newPosition;

        if (position.X > 0)
        {
            newPosition = new Point(position.X - 1, position.Y);

            if (comparer(_map[newPosition.X, newPosition.Y], height) && ! _visited.Contains(newPosition))
            {
                _queue.Enqueue((newPosition, steps + 1), manhattan + steps);

                _visited.Add(newPosition);
            }
        }

        if (position.X < _width - 1)
        {
            newPosition = new Point(position.X + 1, position.Y);

            if (comparer(_map[newPosition.X, newPosition.Y], height) && ! _visited.Contains(newPosition))
            {
                _queue.Enqueue((newPosition, steps + 1), manhattan + steps);

                _visited.Add(newPosition);
            }
        }

        if (position.Y > 0)
        {
            newPosition = new Point(position.X, position.Y - 1);

            if (comparer(_map[newPosition.X, newPosition.Y], height) && ! _visited.Contains(newPosition))
            {
                _queue.Enqueue((newPosition, steps + 1), manhattan + steps);

                _visited.Add(newPosition);
            }
        }

        if (position.Y < _height - 1)
        {
            newPosition = new Point(position.X, position.Y + 1);

            if (comparer(_map[newPosition.X, newPosition.Y], height) && ! _visited.Contains(newPosition))
            {
                _queue.Enqueue((newPosition, steps + 1), manhattan + steps);

                _visited.Add(newPosition);
            }
        }
    }
}