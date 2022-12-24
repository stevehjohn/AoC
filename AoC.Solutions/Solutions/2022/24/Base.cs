using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._24;

public abstract class Base : Solution
{
    public override string Description => "Blizzard basin";

    private Storm[] _storms;

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

        _storms = new Storm[_stormCount];

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

                    continue;
                }

                if (y == Input.Length - 1 && c == '.')
                {
                    _end = new Point(x, y);

                    continue;
                }

                if (c == '#' || c == '.')
                {
                    continue;
                }

                _storms[i] = new Storm(c, x, y);

                i++;
            }
        }
    }

    protected int RunSimulation()
    {
        var queue = new PriorityQueue<(Storm[] Storms, Point Position, int Steps), int>();

        var visited = new HashSet<int>();

        queue.Enqueue((_storms, _start, 1), 0);

        var min = int.MaxValue;

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item.Steps >= min)
            {
                continue;
            }

            Dump(item.Position, item.Storms);

            if (item.Position.Equals(_end))
            {
                if (item.Steps < min)
                {
                    min = item.Steps;

                    Console.WriteLine(min);

                    continue;
                }

                //return item.Steps;
            }

            var nextStorms = MoveStorms(item.Storms);

            var moves = GetMoves(nextStorms, item.Position);

            foreach (var move in moves)
            {
                var hash = new HashCode();

                hash.Add(move.X);
                hash.Add(move.Y);
                hash.Add(item.Steps);

                var code = hash.ToHashCode();

                if (! visited.Contains(code))
                {
                    queue.Enqueue((nextStorms, move, item.Steps + 1), Math.Abs(_end.X - move.X) + Math.Abs(_end.Y - move.Y));

                    visited.Add(code);
                }
            }
        }

        throw new PuzzleException("Answer not found.");
    }

    private void Dump(Point position, Storm[] storms)
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (position.X == x && position.Y == y)
                {
                    Console.Write('E');

                    continue;
                }

                if (storms.Any(s => s.X == x && s.Y == y))
                {
                    Console.Write(storms.First(s => s.X == x && s.Y == y).Direction);

                    continue;
                }

                Console.Write('.');
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    // This'll be sloooooooow...
    private List<Point> GetMoves(Storm[] storms, Point position)
    {
        var moves = new List<Point>();
        
        if (position.X == _end.X && position.Y == _end.Y - 1)
        {
            moves.Add(new Point(position.X, position.Y + 1));

            return moves;
        }

        moves.Add(new Point(position));

        if (position.Y == 0 && position.X == _start.X)
        {
            moves.Add(new Point(position.X, position.Y + 1));

            return moves;
        }

        // TODO: Will need this (pt2).
        //if (position.Y == 1 && position.X == _start.X)
        //{
        //    moves.Add(new Point(position.X, position.Y - 1));
        //}

        if (position.X < _width - 2 && ! storms.Any(s => s.X == position.X + 1 && s.Y == position.Y))
        {
            moves.Add(new Point(position.X + 1, position.Y));
        }

        if (position.X > 1 && ! storms.Any(s => s.X == position.X - 1 && s.Y == position.Y))
        {
            moves.Add(new Point(position.X - 1, position.Y));
        }

        if (position.Y < _height - 2 && ! storms.Any(s => s.X == position.X && s.Y == position.Y + 1))
        {
            moves.Add(new Point(position.X, position.Y + 1));
        }

        if (position.Y > 1 && ! storms.Any(s => s.X == position.X && s.Y == position.Y - 1))
        {
            moves.Add(new Point(position.X, position.Y - 1));
        }

        return moves;
    }

    private Storm[] MoveStorms(Storm[] storms)
    {
        var nextStorms = new Storm[_stormCount];

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
        }

        return nextStorms;
    }
}