using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._20;

public abstract class Base : Solution
{
    public override string Description => "Race condition";

    protected char[,] Map;

    protected int Width;

    protected int Height;

    private Point2D _start;

    private Point2D _end;

    protected State Race()
    {
        var queue = new PriorityQueue<State, int>();

        var visited = new HashSet<Point2D>();
        
        queue.Enqueue(new State(_start, Point2D.North,  1), 1);
        queue.Enqueue(new State(_start, Point2D.East,  1), 1);
        queue.Enqueue(new State(_start, Point2D.South,  1), 1);
        queue.Enqueue(new State(_start, Point2D.West,  1), 1);

        visited.Add(_start);
        
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            var position = state.Position;

            position += state.Direction;

            if (Map[position.X, position.Y] == '#')
            {
                continue;
            }

            if (! visited.Add(position))
            {
                continue;
            }

            if (position == _end)
            {
                return state;
            }

            var steps = state.Steps;
            
            queue.Enqueue(new State(position, Point2D.North, steps + 1), steps + 1);
            queue.Enqueue(new State(position, Point2D.East, steps + 1), steps + 1);
            queue.Enqueue(new State(position, Point2D.South, steps + 1), steps + 1);
            queue.Enqueue(new State(position, Point2D.West, steps + 1), steps + 1);
        }

        return null;
    }

    protected void ParseInput()
    {
        Width = Input[0].Length;

        Height = Input.Length;

        Map = new char[Width, Height];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var c = Input[y][x];

                switch (c)
                {
                    case '#':
                        Map[x, y] = '#';
                        continue;
                    
                    case 'S':
                        _start = new Point2D(x, y);
                        break;
                    
                    case 'E':
                        _end = new Point2D(x, y);
                        break;
                }

                Map[x, y] = '.';
            }
        }
    }
}