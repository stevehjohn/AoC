using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
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

    protected int FindShortestPath()
    {
        var visited = new HashSet<Point>();

        var queue = new PriorityQueue<(Point Position, int Steps, List<Point> History), int>();

        queue.Enqueue((_start, 0, new List<Point> { _start }), 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            var position = node.Position;

            if (position.Equals(_end))
            {
                foreach (var point in node.History)
                {
                    Console.WriteLine($"({point.X}, {point.Y}): {(char) (_map[point.X, point.Y] + 'a')}");
                }

                return node.Steps;
            }

            var height = _map[position.X, position.Y];

            Point newPosition;

            var history = node.History;

            var manhattan = Math.Abs(position.X - _end.X) + Math.Abs(position.Y - _end.Y);

            if (position.X > 0 && _map[position.X - 1, position.Y] <= height + 1)
            {
                newPosition = new Point(position.X - 1, position.Y);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1, new List<Point>(history) { newPosition }), manhattan + node.Steps);

                    visited.Add(newPosition);
                }
            }

            if (position.X < _width - 1 && _map[position.X + 1, position.Y] <= height + 1)
            {
                newPosition = new Point(position.X + 1, position.Y);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1, new List<Point>(history) { newPosition }), manhattan + node.Steps);

                    visited.Add(newPosition);
                }
            }

            if (position.Y > 0 && _map[position.X, position.Y - 1] <= height + 1)
            {
                newPosition = new Point(position.X, position.Y - 1);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1, new List<Point>(history) { newPosition }), manhattan + node.Steps);

                    visited.Add(newPosition);
                }
            }

            if (position.Y < _height - 1 && _map[position.X, position.Y + 1] <= height + 1)
            {
                newPosition = new Point(position.X, position.Y + 1);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, node.Steps + 1, new List<Point>(history) { newPosition }), manhattan + node.Steps);

                    visited.Add(newPosition);
                }
            }
        }

        throw new PuzzleException("Solution not found");
    }
}