using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._16;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly PriorityQueue<State, int> _queue = new();

    private readonly HashSet<int> _bestPaths = [];
    
    private int[] _scores;

    private bool[] _map;

    private int _width;

    private int _length;

    private int _start;

    private int _end;

    public override string GetAnswer()
    {
        ParseInput();

        var score = WalkMaze();
        
        return score.ToString();
    }

    protected int WalkMaze()
    {
        _queue.Clear();
        
        _queue.Enqueue(new State(_start, 1, 0, IsPart2 ? ImmutableStack<int>.Empty : null, 0), 0);
        
        _bestPaths.Clear();

        var bestScore = int.MaxValue;

        _scores = new int[_length << 4];
        
        Array.Fill(_scores, int.MaxValue);

        while (_queue.Count > 0)
        {
            var state = _queue.Dequeue();

            var key = (state.Position << 4) | ((state.Dy + 1) << 2) | (state.Dx + 1);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnqueueMove(State state, int dX, int dY, int scoreChange)
    {
        var position = state.Position + dX + dY * _width;

        var score = state.Score + scoreChange;

        if (! _map[position])
        {
            _queue.Enqueue(new State(position, dX, dY, state.Path, score), score);
        }
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        var height = Input.Length;

        _length = _width * height;

        _map = new bool[_length];

        for (var y = 0; y < height; y++)
        {
            var line = Input[y];
            
            for (var x = 0; x < _width; x++)
            {
                var cell = line[x];

                var index = x + y * _width;

                _map[index] = cell == '#';

                switch (cell)
                {
                    case 'S':
                        _start = index;
                        continue;
                    
                    case 'E':
                        _end = index;
                        continue;
                }
            }
        }
    }
}