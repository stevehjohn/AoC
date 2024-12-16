using System.Collections.Immutable;
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._16;

public abstract class Base : Solution
{
    public override string Description => "Reindeer maze";

    protected bool IsPart2;

    private readonly PriorityQueue<State, int> _queue = new();

    private readonly HashSet<(Point, Point)> _visited = [];

    private readonly HashSet<int> _bestPaths = [];
    
    private int[] _scores;

    private char[] _map;

    private int _width;

    private int _height;

    private int _length;

    private Point _start;

    private Point _end;

    protected int WalkMaze()
    {
        _queue.Clear();
        
        _queue.Enqueue(new State(_start, new Point(1, 0), IsPart2 ? ImmutableStack<int>.Empty : null, 0), 0);
        
        _visited.Clear();
        
        _bestPaths.Clear();

        var bestScore = int.MaxValue;

        if (IsPart2)
        {
            _scores = new int[_length * 100 + 10];
            
            Array.Fill(_scores, int.MaxValue);
        }

        while (_queue.Count > 0)
        {
            var state = _queue.Dequeue();
            
            if (IsPart2)
            {
                var key = (state.Position.Y * _width + state.Position.X) * 100 + state.Direction.Y * 10 + state.Direction.X;

                if (_scores[key] < state.Score)
                {
                    continue;
                }

                _scores[key] = state.Score;
            }
            else
            {
                if (! _visited.Add((state.Position, state.Direction)))
                {
                    continue;
                }
            }

            if (IsPart2)
            {
                state.Path = state.Path.Push(state.Position.X + state.Position.Y * _width);
            }

            if (state.Position.Equals(_end))
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

                var stack = state.Path;
                
                while (! stack.IsEmpty)
                {
                    _bestPaths.Add(stack.Peek());

                    stack = stack.Pop();
                }
            }

            EnqueueMove(state, state.Direction, 1);
            
            EnqueueMove(state, new Point(-state.Direction.Y, state.Direction.X) , 1_001);
            
            EnqueueMove(state, new Point(state.Direction.Y, -state.Direction.X), 1_001);
        }
        
        return -1;
    }

    private void EnqueueMove(State state, Point direction, int scoreChange)
    {
        state.Position += direction;

        state.Score += scoreChange;
        
        if (_map[state.Position.X + state.Position.Y * _width] == '.')
        {
            _queue.Enqueue(new State(state.Position, direction, state.Path, state.Score), state.Score);
        }
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _length = _width * _height;

        _map = new char[_length];

        for (var y = 0; y < _height; y++)
        {
            var line = Input[y];
            
            for (var x = 0; x < _width; x++)
            {
                var cell = line[x];

                var index = x + y * _width;

                switch (cell)
                {
                    case 'S':
                        _start = new Point(x, y);
                        _map[index] = '.';
                        continue;
                    
                    case 'E':
                        _end = new Point(x, y);
                        _map[index] = '.';
                        continue;
                    
                    default:
                        _map[index] = cell;
                        break;
                }
            }
        }
    }
}