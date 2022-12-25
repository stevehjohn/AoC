using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._24;

// TODO: This class makes my eyes bleed. Needs tidying up!
public abstract class Base : Solution
{
    public override string Description => "Blizzard basin";

    private Storm[] _initialStorms;

    private HashSet<int> _initialHashes;

    private int _stormCount;

    private int _width;

    private int _height;

    private Point _start;

    private Point _end;

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        for (var y = 1; y < _height - 1; y++)
        {
            var line = Input[y];

            for (var x = 1; x < _width - 1; x++)
            {
                if (line[x] != '.')
                {
                    _stormCount++;
                }
            }
        }

        _initialStorms = new Storm[_stormCount];

        _initialHashes = new HashSet<int>(_stormCount);

        var i = 0;

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

                _initialStorms[i] = new Storm(c, x, y);

                _initialHashes.Add(HashCode.Combine(_initialStorms[i].X, _initialStorms[i].Y));

                i++;
            }
        }
    }

    protected int RunSimulation(int loops = 1)
    {
        var queue = new PriorityQueue<(Storm[] Storms, HashSet<int> Hashes, Point Position, int Steps), int>();

        var visited = new HashSet<int>();

        queue.Enqueue((_initialStorms, _initialHashes, _start, 0), 0);

        var origin = _start;

        var target = _end;

        var totalMin = 0;

        var min = int.MaxValue;

        while (loops > 0)
        {
            Storm[] lastStorms = null;

            HashSet<int> lastHashes = null;

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();

                if (item.Steps >= min)
                {
                    continue;
                }

                if (item.Position.Equals(target))
                {
                    if (item.Steps < min)
                    {
                        min = item.Steps;

                        lastStorms = item.Storms;

                        lastHashes = item.Hashes;

                        break;
                    }
                }

                var (nextStorms, nextHashes) = MoveStorms(item.Storms);

                var moves = GetMoves(nextStorms, nextHashes, item.Position, target, origin);

                foreach (var move in moves)
                {
                    if (nextStorms.Any(s => s.X == move.X && s.Y == move.Y))
                    {
                        continue;
                    }

                    var hash = new HashCode();

                    hash.Add(move.X);
                    hash.Add(move.Y);
                    hash.Add(item.Steps);

                    var code = hash.ToHashCode();

                    if (! visited.Contains(code))
                    {
                        queue.Enqueue((nextStorms, nextHashes, move, item.Steps + 1), Math.Abs(target.X - move.X) + Math.Abs(target.Y - move.Y) + item.Steps);

                        visited.Add(code);
                    }
                }
            }

            queue.Clear();

            loops--;

            totalMin += min;

            visited.Clear();

            if (loops > 0)
            {
                if (target.Equals(_end))
                {
                    target = _start;

                    origin = _end;

                    queue.Enqueue((lastStorms, lastHashes, _end, 0), 0);
                }
                else
                {
                    target = _end;

                    origin = _start;

                    queue.Enqueue((lastStorms, lastHashes, _start, 0), 0);
                }

                min = int.MaxValue;
            }
        }

        return Math.Max(min, totalMin);
    }

    private List<Point> GetMoves(Storm[] storms, HashSet<int> hashes, Point position, Point target, Point origin)
    {
        var moves = new List<Point>();

        // Reached goal (end).
        if (target.Equals(_end) && position.X == target.X && position.Y == target.Y - 1)
        {
            moves.Add(new Point(position.X, position.Y + 1));

            return moves;
        }

        // Reached goal (start).
        if (target.Equals(_start) && position.X == target.X && position.Y == target.Y + 1)
        {
            moves.Add(new Point(position.X, position.Y - 1));

            return moves;
        }

        // Loiter.
        if (! storms.Any(s => s.X == position.X && s.Y == position.Y))
        {
            moves.Add(new Point(position));
        }

        // In and out of start/end.
        if (origin.Equals(_start))
        {
            if (position.Y == 0 && position.X == origin.X)
            {
                moves.Add(new Point(position.X, position.Y + 1));

                return moves;
            }

            if (position.Y == 1 && position.X == origin.X)
            {
                moves.Add(new Point(position.X, position.Y - 1));
            }
        }
        else
        {
            if (position.Y == _height - 1 && position.X == origin.X)
            {
                moves.Add(new Point(position.X, position.Y - 2));

                return moves;
            }

            if (position.Y == _height - 2 && position.X == origin.X)
            {
                moves.Add(new Point(position.X, position.Y + 1));
            }
        }

        // Right?
        if (position.X < _width - 2 && ! hashes.Contains(HashCode.Combine(position.X + 1, position.Y)))
        {
            moves.Add(new Point(position.X + 1, position.Y));
        }

        // Left?
        if (position.X > 1 && ! hashes.Contains(HashCode.Combine(position.X - 1, position.Y)))
        {
            moves.Add(new Point(position.X - 1, position.Y));
        }

        // Down?
        if (position.Y < _height - 2 && ! hashes.Contains(HashCode.Combine(position.X, position.Y + 1)))
        {
            moves.Add(new Point(position.X, position.Y + 1));
        }

        // Up?
        if (position.Y > 1 && ! hashes.Contains(HashCode.Combine(position.X, position.Y - 1)))
        {
            moves.Add(new Point(position.X, position.Y - 1));
        }

        return moves;
    }

    private (Storm[] Storms, HashSet<int> Hashes) MoveStorms(Storm[] storms)
    {
        var nextStorms = new Storm[_stormCount];

        var nextHashes = new HashSet<int>(_stormCount);

        for (var i = 0; i < _stormCount; i++)
        {
            var storm = storms[i];

            int x;

            int y;

            switch (storm.Direction)
            {
                case '^':
                    y = storm.Y - 1;

                    if (y == 0)
                    {
                        y = _height - 2;
                    }

                    nextStorms[i] = new Storm(storm.Direction, storm.X, y);

                    continue;

                case '>':
                    x = storm.X + 1;

                    if (x == _width - 1)
                    {
                        x = 1;
                    }

                    nextStorms[i] = new Storm(storm.Direction, x, storm.Y);

                    continue;

                case 'v':
                    y = storm.Y + 1;

                    if (y == _height - 1)
                    {
                        y = 1;
                    }

                    nextStorms[i] = new Storm(storm.Direction, storm.X, y);

                    continue;

                case '<':
                    x = storm.X - 1;

                    if (x == 0)
                    {
                        x = _width - 2;
                    }

                    nextStorms[i] = new Storm(storm.Direction, x, storm.Y);

                    continue;
            }

            nextHashes.Add(HashCode.Combine(nextStorms[i].X, nextStorms[i].Y));
        }

        return (nextStorms, nextHashes);
    }
}