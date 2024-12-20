using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._20;

public abstract class Base : Solution
{
    public override string Description => "Race condition";

    private char[,] _map;

    private int _width;

    private int _height;

    private Point2D _start;

    private Point2D _end;

    protected int Race()
    {
        var queue = new PriorityQueue<(Point2D Position, Point2D Direction, int Steps), int>();

        var visited = new HashSet<(Point2D, Point2D)>();
        
        queue.Enqueue((_start, Point2D.North,  1), 1);
        queue.Enqueue((_start, Point2D.East,  1), 1);
        queue.Enqueue((_start, Point2D.South,  1), 1);
        queue.Enqueue((_start, Point2D.West,  1), 1);

        visited.Add((_start, Point2D.North));
        
        while (queue.Count > 0)
        {
            var (position, direction, steps) = queue.Dequeue();

            position += direction;

            if (_map[position.X, position.Y] == '#')
            {
                continue;
            }

            if (! visited.Add((position, direction)))
            {
                continue;
            }

            if (position == _end)
            {
                return steps;
            }
            
            queue.Enqueue((position, Point2D.North, steps + 1), steps + 1);
            queue.Enqueue((position, Point2D.East, steps + 1), steps + 1);
            queue.Enqueue((position, Point2D.South, steps + 1), steps + 1);
            queue.Enqueue((position, Point2D.West, steps + 1), steps + 1);
        }

        return -1;
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var c = Input[y][x];

                switch (c)
                {
                    case '#':
                        _map[x, y] = '#';
                        continue;
                    
                    case 'S':
                        _start = new Point2D(x, y);
                        break;
                    
                    case 'E':
                        _end = new Point2D(x, y);
                        break;
                }

                _map[x, y] = '.';
            }
        }
    }
}