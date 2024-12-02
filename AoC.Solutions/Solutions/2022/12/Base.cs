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

    private readonly HashSet<Point> _visited = [];

    private readonly PriorityQueue<(Point Position, int Steps, List<Point> History), int> _queue = new();
    
    private readonly IVisualiser<PuzzleState> _visualiser;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

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

                switch (character)
                {
                    case 'S':
                        _start = new Point(x, y);

                        _map[x, y] = 0;

                        continue;
                    case 'E':
                        _end = new Point(x, y);

                        _map[x, y] = 'z' - 'a';

                        continue;
                    default:
                        _map[x, y] = (byte) (character - 'a');
                        break;
                }
            }
        }
    }

    protected int FindShortestPath(bool startFromEnd = false)
    {
        Visualise();

        if (startFromEnd)
        {
            _queue.Enqueue((_end, 0, [_end]), 0);
        }
        else
        {
            _queue.Enqueue((_start, 0, [_start]), 0);
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
            var (position, steps, history) = _queue.Dequeue();

            Visualise(history);

            if (startFromEnd)
            {
                if (_map[position.X, position.Y] == 0)
                {
                    return steps;
                }
            }
            else
            {
                if (position.Equals(_end))
                {
                    return steps;
                }
            }

            var height = _map[position.X, position.Y];

            var manhattan = 0;

            if (! startFromEnd)
            {
                manhattan = Math.Abs(position.X - _end.X) + Math.Abs(position.Y - _end.Y);
            }

            AddPossibleMoves(position, comparer, height, steps, manhattan, history);
        }

        return int.MaxValue;
    }

    private void AddPossibleMoves(Point position, Func<byte, byte, bool> comparer, byte height, int steps, int manhattan, List<Point> history)
    {
        Point newPosition;

        if (position.X > 0)
        {
            newPosition = new Point(position.X - 1, position.Y);

            if (comparer(_map[newPosition.X, newPosition.Y], height) && ! _visited.Contains(newPosition))
            {
                _queue.Enqueue((newPosition, steps + 1, [..history, newPosition]), manhattan + steps);

                _visited.Add(newPosition);
            }
        }

        if (position.X < _width - 1)
        {
            newPosition = new Point(position.X + 1, position.Y);

            if (comparer(_map[newPosition.X, newPosition.Y], height) && ! _visited.Contains(newPosition))
            {
                _queue.Enqueue((newPosition, steps + 1, [..history, newPosition]), manhattan + steps);

                _visited.Add(newPosition);
            }
        }

        if (position.Y > 0)
        {
            newPosition = new Point(position.X, position.Y - 1);

            if (comparer(_map[newPosition.X, newPosition.Y], height) && ! _visited.Contains(newPosition))
            {
                _queue.Enqueue((newPosition, steps + 1, [..history, newPosition]), manhattan + steps);

                _visited.Add(newPosition);
            }
        }

        if (position.Y < _height - 1)
        {
            newPosition = new Point(position.X, position.Y + 1);

            if (comparer(_map[newPosition.X, newPosition.Y], height) && ! _visited.Contains(newPosition))
            {
                _queue.Enqueue((newPosition, steps + 1, [..history, newPosition]), manhattan + steps);

                _visited.Add(newPosition);
            }
        }
    }

    private void Visualise(List<Point> history = null)
    {
        _visualiser?.PuzzleStateChanged(new PuzzleState(_map, history));
    }
}