using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._16;

public abstract class Base : Solution
{
    public override string Description => "Reindeer maze";

    protected bool IsPart2;

    private readonly PriorityQueue<(Point Positon, Point Direction, HashSet<Point> Path, int Score), int> _queue = new();

    private readonly HashSet<(Point, Point)> _visited = [];

    private readonly HashSet<Point> _bestPaths = [];

    private char[,] _map;

    private int _width;

    private int _height;

    private Point _start;

    private Point _end;

    protected int WalkMaze()
    {
        _queue.Clear();
        
        _queue.Enqueue((_start, new Point(1, 0), IsPart2 ? [] : null, 0), 0);
        
        _visited.Clear();

        var bestScore = int.MaxValue;

        while (_queue.Count > 0)
        {
            var state = _queue.Dequeue();

            if (! _visited.Add((state.Positon, state.Direction)) && ! IsPart2)
            {
                continue;
            }

            state.Path?.Add(state.Positon);

            if (state.Positon.Equals(_end))
            {
                if (state.Path == null)
                {
                    return state.Score;
                }

                bestScore = Math.Min(bestScore, state.Score);

                if (state.Score > bestScore)
                {
                    return _bestPaths.Count;
                }
                
                Console.WriteLine(state.Path.Count);
                
                _bestPaths.UnionWith(state.Path);
            }

            EnqueueMove(state.Positon, state.Direction, state.Path, state.Score, 1);
            
            EnqueueMove(state.Positon, new Point(-state.Direction.Y, state.Direction.X), state.Path, state.Score, 1_001);
            
            EnqueueMove(state.Positon, new Point(state.Direction.Y, -state.Direction.X), state.Path, state.Score, 1_001);
        }
        
        return -1;
    }

    private void EnqueueMove(Point position, Point direction, HashSet<Point> path, int score, int scoreChange)
    {
        position += direction;

        score += scoreChange;
        
        if (_map[position.X, position.Y] == '.')
        {
            _queue.Enqueue((position, direction, path == null ? null : [..path], score), score);
        }
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            var line = Input[y];
            
            for (var x = 0; x < _width; x++)
            {
                var cell = line[x];

                switch (cell)
                {
                    case 'S':
                        _start = new Point(x, y);
                        _map[x, y] = '.';
                        continue;
                    
                    case 'E':
                        _end = new Point(x, y);
                        _map[x, y] = '.';
                        continue;
                    
                    default:
                        _map[x, y] = cell;
                        break;
                }
            }
        }
    }
}