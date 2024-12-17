using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._16;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly PriorityQueue<State, int> _queue = new();

    private bool[] _visited;
    
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

    private int WalkMaze()
    {
        _queue.Clear();
        
        _queue.Enqueue(new State(_start, 1, 0, 0), 0);

        _visited = new bool[_length << 4];

        while (_queue.Count > 0)
        {
            var state = _queue.Dequeue();

            var key = (state.Position << 4) | ((state.Dy + 1) << 2) | (state.Dx + 1);

            if (_visited[key])
            {
                continue;
            }

            _visited[key] = true;

            if (state.Position == _end)
            {
                return state.Score;
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
            _queue.Enqueue(new State(position, dX, dY, score), score);
        }
    }

    private void ParseInput()
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