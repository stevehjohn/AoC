using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._24;

public abstract class Base : Solution
{
    public override string Description => "Blizzard basin";

    private readonly Dictionary<int, HashSet<int>> _leftStorms = new();

    private readonly Dictionary<int, HashSet<int>> _downStorms = new();

    private readonly Dictionary<int, HashSet<int>> _rightStorms = new();

    private readonly Dictionary<int, HashSet<int>> _upStorms = new();

    private int _width;

    private int _height;

    private int _blizzardWidth;

    private int _blizzardHeight;

    private Point _start;

    private Point _end;

    private readonly Dictionary<int, bool> _occupiedCache = new(600_000);

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _blizzardWidth = _width - 2;

        _blizzardHeight = _height - 2;

        for (var y = 0; y < _height; y++)
        {
            var line = Input[y];

            _leftStorms.Add(y, new HashSet<int>());

            _rightStorms.Add(y, new HashSet<int>());

            for (var x = 0; x < _width; x++)
            {
                if (! _downStorms.ContainsKey(x))
                {
                    _downStorms.Add(x, new HashSet<int>());

                    _upStorms.Add(x, new HashSet<int>());
                }

                var c = line[x];

                if (y == 0 && c == '.')
                {
                    _start = new Point(x, y);
                }

                if (y == Input.Length - 1 && c == '.')
                {
                    _end = new Point(x, y);
                }

                if (c == '#' || c == '.')
                {
                    continue;
                }

                if (c == '<')
                {
                    _leftStorms[y].Add(x);

                    continue;
                }

                if (c == '>')
                {
                    _rightStorms[y].Add(x);

                    continue;
                }

                if (c == 'v')
                {
                    _downStorms[x].Add(y);
                }

                if (c == '^')
                {
                    _upStorms[x].Add(y);
                }
            }
        }
    }

    protected int RunSimulation(int loops = 1)
    {
        var steps = 0;

        for (var i = 0; i < loops; i++)
        {
            steps += RunSimulationStep(steps);
        }

        return steps;
    }

    private int RunSimulationStep(int startIteration)
    {
        var queue = new PriorityQueue<(Point Position, int Steps), int>();

        var visited = new HashSet<int>();

        queue.Enqueue((new Point(_start.X, _start.Y), 0), 0);

        var origin = _start;

        var target = _end;

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item.Position.X == target.X && item.Position.Y == target.Y)
            {
                return item.Steps;
            }

            var moves = GenerateMoves(item.Position.X, item.Position.Y, target, origin, startIteration + item.Steps + 1);

            foreach (var move in moves)
            {
                var hash = new HashCode();

                hash.Add(move.X);
                hash.Add(move.Y);
                hash.Add(item.Steps);

                var code = hash.ToHashCode();

                if (! visited.Contains(code))
                {
                    queue.Enqueue((new Point(move), item.Steps + 1), Math.Abs(target.X - move.X) + Math.Abs(target.Y - move.Y) + item.Steps);

                    visited.Add(code);
                }
            }
        }

        throw new PuzzleException("Solution not found.");
    }

    private List<Point> GenerateMoves(int x, int y, Point target, Point origin, int iteration)
    {
        var moves = new List<Point>();

        // Reached goal (end).
        if (target.Equals(_end) && x == target.X && y == target.Y - 1)
        {
            moves.Add(new Point(x, y + 1));

            return moves;
        }

        // Reached goal (start).
        if (target.Equals(_start) && x == target.X && y == target.Y + 1)
        {
            moves.Add(new Point(x, y - 1));

            return moves;
        }

        // Loiter.
        if (! IsOccupied(new Point(x, y), iteration))
        {
            moves.Add(new Point(x, y));
        }

        // In and out of start/end.
        if (origin.Equals(_start))
        {
            if (y == 0 && x == origin.X)
            {
                moves.Add(new Point(x, 1));

                return moves;
            }

            if (y == 1 && x == origin.X)
            {
                moves.Add(new Point(x, y - 1));
            }
        }
        else
        {
            if (y == _height - 1 && x == origin.X)
            {
                moves.Add(new Point(x, y - 2));

                return moves;
            }

            if (y == _height - 2 && x == origin.X)
            {
                moves.Add(new Point(x, y + 1));
            }
        }

        // Right?
        if (x < _blizzardWidth && ! IsOccupied(new Point(x + 1, y), iteration))
        {
            moves.Add(new Point(x + 1, y));
        }

        // Left?
        if (x > 1 && ! IsOccupied(new Point(x - 1, y), iteration))
        {
            moves.Add(new Point(x - 1, y));
        }

        // Down?
        if (y < _blizzardHeight && ! IsOccupied(new Point(x, y + 1), iteration))
        {
            moves.Add(new Point(x, y + 1));
        }

        // Up?
        if (y > 1 && ! IsOccupied(new Point(x, y - 1), iteration))
        {
            moves.Add(new Point(x, y - 1));
        }

        return moves;
    }

    private bool IsOccupied(Point position, int iteration)
    {
        var key = HashCode.Combine(position, iteration);

        if (_occupiedCache.TryGetValue(key, out var occupied))
        {
            return occupied;
        }

        if (position.X < 1 || position.Y < 1 || position.X >= _width || position.Y >= _height)
        {
            _occupiedCache.Add(key, false);

            return false;
        }

        var xD = iteration % _blizzardWidth;

        var yD = iteration % _blizzardHeight;

        var target = (position.X - 1 + _blizzardWidth - xD) % _blizzardWidth + 1;

        var found = _rightStorms[position.Y].Contains(target);

        if (found)
        {
            _occupiedCache.Add(key, true);

            return true;
        }

        target = (position.X - 1 + xD) % _blizzardWidth + 1;

        found = _leftStorms[position.Y].Contains(target);

        if (found)
        {
            _occupiedCache.Add(key, true);

            return true;
        }

        target = (position.Y - 1 + _blizzardHeight - yD) % _blizzardHeight + 1;

        found = _downStorms[position.X].Contains(target);

        if (found)
        {
            _occupiedCache.Add(key, true);

            return true;
        }

        target = (position.Y - 1 + yD) % _blizzardHeight + 1;

        found = _upStorms[position.X].Contains(target);
        
        _occupiedCache.Add(key, found);

        return found;
    }
}