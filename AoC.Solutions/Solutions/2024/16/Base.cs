using System.Collections.Immutable;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._16;

public abstract class Base : Solution
{
    public override string Description => "Reindeer maze";

    protected bool IsPart2;

    private readonly PriorityQueue<State, int> _queue = new();

    private readonly HashSet<int> _bestPaths = [];
    
    private int[] _scores;

    private char[] _map;

    private int _width;

    private int _height;

    private int _length;

    private int _start;

    private int _end;

    protected int WalkMaze()
    {
        _queue.Clear();
        
        _queue.Enqueue(new State(_start, 1, 0, IsPart2 ? ImmutableStack<int>.Empty : null, 0), 0);
        
        _bestPaths.Clear();

        var bestScore = int.MaxValue;

        _scores = new int[_length * 100 + 10];
        
        Array.Fill(_scores, int.MaxValue);

        while (_queue.Count > 0)
        {
            var state = _queue.Dequeue();

            var key = state.Position * 100 + state.Dy * 10 + state.Dx;

            if ((_scores[key] != int.MaxValue && ! IsPart2) || _scores[key] < state.Score)
            {
                continue;
            }

            _scores[key] = state.Score;

            if (IsPart2)
            {
                state.Path = state.Path.Push(state.Position);
            }

            if (state.Position == _end)
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

            EnqueueMove(state, state.Dx, state.Dy, 1);

            EnqueueMove(state, -state.Dy, state.Dx, 1_001);

            EnqueueMove(state, state.Dy, -state.Dx, 1_001);
        }

        return -1;
    }

    private void EnqueueMove(State state, int dX, int dY, int scoreChange)
    {
        var position = state.Position + dX + dY * _width;
        
        if (_map[position] == '.')
        {
            _queue.Enqueue(new State(position, dX, dY, state.Path, state.Score + scoreChange), state.Score + scoreChange);
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
                        _start = x + y * _width;
                        _map[index] = '.';
                        continue;
                    
                    case 'E':
                        _end = x + y * _width;
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