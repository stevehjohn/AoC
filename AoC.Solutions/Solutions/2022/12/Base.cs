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
        var visited = new HashSet<Point>();

        var queue = new PriorityQueue<(Point Position, int Steps), int>();

        if (startFromEnd)
        {
            queue.Enqueue((_end, 0), 0);
        }
        else
        {
            queue.Enqueue((_start, 0), 0);
        }

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

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

            Point newPosition;

            var manhattan = 0;

            if (! startFromEnd)
            {
                manhattan = Math.Abs(position.X - _end.X) + Math.Abs(position.Y - _end.Y);
            }

            if (position.X > 0 && _map[position.X - 1, position.Y] <= height + 1)
            {
                newPosition = new Point(position.X - 1, position.Y);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1), manhattan + node.Steps);

                    visited.Add(newPosition);
                }
            }

            if (position.X < _width - 1 && _map[position.X + 1, position.Y] <= height + 1)
            {
                newPosition = new Point(position.X + 1, position.Y);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1), manhattan + node.Steps);

                    visited.Add(newPosition);
                }
            }

            if (position.Y > 0 && _map[position.X, position.Y - 1] <= height + 1)
            {
                newPosition = new Point(position.X, position.Y - 1);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1), manhattan + node.Steps);

                    visited.Add(newPosition);
                }
            }

            if (position.Y < _height - 1 && _map[position.X, position.Y + 1] <= height + 1)
            {
                newPosition = new Point(position.X, position.Y + 1);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1), manhattan + node.Steps);

                    visited.Add(newPosition);
                }
            }
        }

        return int.MaxValue;
    }

    protected int FindShortestPath2()
    {
        var visited = new HashSet<Point>();

        var queue = new PriorityQueue<(Point Position, int Steps), int>();

        queue.Enqueue((_end, 0), 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            var position = node.Position;

            if (_map[position.X, position.Y] == 0)
            {
                return node.Steps;
            }

            var height = _map[position.X, position.Y];

            Point newPosition;

            if (position.X > 0 && _map[position.X - 1, position.Y] >= height - 1)
            {
                newPosition = new Point(position.X - 1, position.Y);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1), node.Steps);

                    visited.Add(newPosition);
                }
            }

            if (position.X < _width - 1 && _map[position.X + 1, position.Y] >= height - 1)
            {
                newPosition = new Point(position.X + 1, position.Y);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1), node.Steps);

                    visited.Add(newPosition);
                }
            }

            if (position.Y > 0 && _map[position.X, position.Y - 1] >= height - 1)
            {
                newPosition = new Point(position.X, position.Y - 1);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1), node.Steps);

                    visited.Add(newPosition);
                }
            }

            if (position.Y < _height - 1 && _map[position.X, position.Y + 1] >= height - 1)
            {
                newPosition = new Point(position.X, position.Y + 1);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1), node.Steps);

                    visited.Add(newPosition);
                }
            }
        }

        return int.MaxValue;
    }
}