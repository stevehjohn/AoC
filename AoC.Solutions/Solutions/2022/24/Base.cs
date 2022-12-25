using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._24;

public abstract class Base : Solution
{
    public override string Description => "Blizzard basin";

    private readonly Dictionary<int, List<Storm>> _horizontalStorms = new();

    private readonly Dictionary<int, List<Storm>> _verticalStorms = new();

    private int _width;

    private int _height;

    private int _blizzardWidth;

    private int _blizzardHeight;

    private Point _start;

    private Point _end;

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

                if (c is '<' or '>')
                {
                    if (! _horizontalStorms.ContainsKey(y))
                    {
                        _horizontalStorms.Add(y, new List<Storm>());
                    }

                    _horizontalStorms[y].Add(new Storm(c, x, y));
                }
                else
                {
                    if (! _verticalStorms.ContainsKey(x))
                    {
                        _verticalStorms.Add(x, new List<Storm>());
                    }

                    _verticalStorms[x].Add(new Storm(c, x, y));
                }
            }
        }
    }

    protected int RunSimulation(int loops = 1)
    {
        var steps = 0;

        for (var l = 0; l < loops; l++)
        {
            if (l % 2 == 0)
            {
                steps += RunSimulationLoop(steps, _start, _end);
            }
            else
            {
                steps += RunSimulationLoop(steps, _end, _start);
            }
        }

        return steps;
    }

    protected int RunSimulationLoop(int startStep, Point origin, Point target)
    {
        var queue = new PriorityQueue<(Point Position, int Steps), int>();

        var visited = new HashSet<int>();

        queue.Enqueue((new Point(_start.X, _start.Y), startStep), 0);

        var min = int.MaxValue;

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item.Steps >= min)
            {
                continue;
            }

            if (item.Position.X == target.X && item.Position.Y == target.Y)
            {
                min = item.Steps;

                Console.WriteLine(min);

                break;
            }

            var moves = GenerateMoves(item.Position.X, item.Position.Y, target, origin, item.Steps + 1);

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

        return min;
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
        if (position.X < 1 || position.Y < 1 || position.X >= _width || position.Y >= _height)
        {
            return false;
        }

        var xD = iteration % _blizzardWidth;

        var yD = iteration % _blizzardHeight;

        if (! _horizontalStorms.ContainsKey(position.Y))
        {
            return false;
        }

        var found = _horizontalStorms[position.Y].Any(s => position.X == s.Direction switch
        {
            '>' => (s.X - 1 + xD) % _blizzardWidth + 1,
            '<' => (s.X - 1 + _blizzardWidth - xD) % _blizzardWidth + 1,
            _ => throw new PuzzleException("This exception shouldn't happen.")
        });

        if (found)
        {
            return true;
        }

        if (! _verticalStorms.ContainsKey(position.X))
        {
            return false;
        }

        found = _verticalStorms[position.X].Any(s => position.Y == s.Direction switch
        {
            'v' => (s.Y - 1 + yD) % _blizzardHeight + 1,
            '^' => (s.Y - 1 + _blizzardHeight - yD) % _blizzardHeight + 1,
            _ => throw new PuzzleException("This exception shouldn't happen.")
        });

        return found;
    }
}