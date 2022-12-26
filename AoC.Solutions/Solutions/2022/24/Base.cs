using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._24;

public abstract class Base : Solution
{
    public override string Description => "Blizzard basin";

    private readonly Dictionary<int, List<Storm>> _leftStorms = new();

    private readonly Dictionary<int, List<Storm>> _downStorms = new();

    private readonly Dictionary<int, List<Storm>> _rightStorms = new();

    private readonly Dictionary<int, List<Storm>> _upStorms = new();

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

            for (var x = 0; x < _width; x++)
            {
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
                    if (! _leftStorms.ContainsKey(y))
                    {
                        _leftStorms.Add(y, new List<Storm>());
                    }

                    _leftStorms[y].Add(new Storm(c, x, y));

                    continue;
                }

                if (c == '>')
                {
                    if (! _rightStorms.ContainsKey(y))
                    {
                        _rightStorms.Add(y, new List<Storm>());
                    }

                    _rightStorms[y].Add(new Storm(c, x, y));

                    continue;
                }

                if (c == 'v')
                {
                    if (! _downStorms.ContainsKey(x))
                    {
                        _downStorms.Add(x, new List<Storm>());
                    }

                    _downStorms[x].Add(new Storm(c, x, y));
                }

                if (c == '^')
                {
                    if (! _upStorms.ContainsKey(x))
                    {
                        _upStorms.Add(x, new List<Storm>());
                    }

                    _upStorms[x].Add(new Storm(c, x, y));
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

        if (_occupiedCache.ContainsKey(key))
        {
            return _occupiedCache[key];
        }

        if (position.X < 1 || position.Y < 1 || position.X >= _width || position.Y >= _height)
        {
            _occupiedCache.Add(key, false);

            return false;
        }

        var xD = iteration % _blizzardWidth;

        var yD = iteration % _blizzardHeight;

        var target = (position.X - 1 + _blizzardWidth - xD) % _blizzardWidth + 1;

        var found = _rightStorms.ContainsKey(position.Y) && _rightStorms[position.Y].Any(s => target == s.X);

        if (found)
        {
            _occupiedCache.Add(key, true);

            return true;
        }

        target = (position.X - 1 + xD) % _blizzardWidth + 1;

        found = _leftStorms.ContainsKey(position.Y) && _leftStorms[position.Y].Any(s => target == s.X);

        if (found)
        {
            _occupiedCache.Add(key, true);

            return true;
        }

        target = (position.Y - 1 + _blizzardHeight - yD) % _blizzardHeight + 1;

        found = _downStorms.ContainsKey(position.X) && _downStorms[position.X].Any(s => target == s.Y);

        if (found)
        {
            _occupiedCache.Add(key, true);

            return true;
        }

        target = (position.Y - 1 + yD) % _blizzardHeight + 1;

        found = _upStorms.ContainsKey(position.X) && _upStorms[position.X].Any(s => target == s.Y);
        
        _occupiedCache.Add(key, found);

        return found;
    }
}