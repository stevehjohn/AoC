using System.Numerics;
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._16;

public abstract class Base : Solution
{
    public override string Description => "Reindeer maze";

    protected bool IsPart2;

    private readonly PriorityQueue<(Point Position, Point Direction, byte[] Path, int Score), int> _queue = new();

    private readonly HashSet<(Point, Point, int)> _visited = [];

    private int[] _visitCounts;

    private byte[] _bestPaths;

    private char[] _map;

    private int _width;

    private int _height;

    private int _length;

    private Point _start;

    private Point _end;

    protected int WalkMaze()
    {
        _queue.Clear();
        
        _queue.Enqueue((_start, new Point(1, 0), IsPart2 ? new byte[_width * _height / 8] : null, 0), 0);
        
        _visited.Clear();

        var bestScore = int.MaxValue;

        if (IsPart2)
        {
            _bestPaths = new byte[_length / 8];

            _visitCounts = new int[_length * 100 + 10];
        }

        while (_queue.Count > 0)
        {
            var state = _queue.Dequeue();
            
            if (IsPart2)
            {
                var key = (state.Position.Y * _width + state.Position.X) * 100 + state.Direction.Y * 10 + state.Direction.X;

                _visitCounts[key]++;
            }
            else
            {
                if (! _visited.Add((state.Position, state.Direction, 1)))
                {
                    continue;
                }
            }

            if (IsPart2)
            {
                var newPath = new byte[_length / 8];
                
                Buffer.BlockCopy(state.Path, 0, newPath, 0, sizeof(byte) * _length / 8);

                newPath[(state.Position.X + state.Position.Y * _width) / 8] |= (byte) (1 << ((state.Position.X + state.Position.Y * _width) % 8));

                state.Path = newPath;
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
                    var count = 0;
                    
                    for (var i = 0; i < _length / 8; i++)
                    {
                        count += BitOperations.PopCount(_bestPaths[i]);
                    }

                    return count;
                }
                
                for (var i = 0; i < _length / 8; i++)
                {
                    _bestPaths[i] |= state.Path[i];
                }
            }

            EnqueueMove(state.Position, state.Direction, state.Path, state.Score, 1);
            
            EnqueueMove(state.Position, new Point(-state.Direction.Y, state.Direction.X), state.Path, state.Score, 1_001);
            
            EnqueueMove(state.Position, new Point(state.Direction.Y, -state.Direction.X), state.Path, state.Score, 1_001);
        }
        
        return -1;
    }

    private void EnqueueMove(Point position, Point direction, byte[] path, int score, int scoreChange)
    {
        position += direction;

        score += scoreChange;
        
        if (_map[position.X + position.Y * _width] == '.')
        {
            var penalty = 0;

            if (IsPart2)
            {
                var key = (position.Y * _width + position.X) * 100 + direction.Y * 10 + direction.X;
                
                penalty = _visitCounts[key] * 1_000_000;
            }

            _queue.Enqueue((position, direction, path, score), score + penalty);
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