using System.Diagnostics;
using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._24;

// TODO: This class makes my eyes bleed. Needs tidying up!
public abstract class Base : Solution
{
    public override string Description => "Blizzard basin";

    private const int MaxPossibleMoves = 5;

    private Storm[] _initialStorms;

    private int _stormCount;

    private int _width;

    private int _height;

    private int _blizzardWidth;

    private int _blizzardHeight;

    private Point _start;

    private Point _end;

    private readonly Point[] _possibleMoves = new Point[MaxPossibleMoves];

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _blizzardWidth = _width - 2;

        _blizzardHeight = _height - 2;

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

                i++;
            }
        }
    }

    protected int RunSimulation(int loops = 1)
    {
        for (var i = 0; i < MaxPossibleMoves; i++)
        {
            _possibleMoves[i] = new Point();
        }

        var queue = new PriorityQueue<(Point Position, int Steps), int>();

        var visited = new HashSet<int>();

        queue.Enqueue((new Point(_start.X, _start.Y), 0), 0);

        var origin = _start;

        var target = _end;

        var totalMin = 0;

        var min = int.MaxValue;

        //for (var i = 0; i < 20; i++)
        //{
        //    Dump(i);
        //}

        while (loops > 0)
        {
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

                var moveCount = GenerateMoves(item.Position.X, item.Position.Y, target, origin, totalMin + item.Steps + 1);

                for (var i = 0; i < moveCount; i++)
                {
                    var move = _possibleMoves[i];

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

                    queue.Enqueue((new Point(_end.X, _end.Y), 0), 0);
                }
                else
                {
                    target = _end;

                    origin = _start;

                    queue.Enqueue((new Point(_start.X, _start.Y), 0), 0);
                }

                min = int.MaxValue;
            }
        }

        return Math.Max(min, totalMin);
    }

    private void Dump(int iteration)
    {
        Console.WriteLine(iteration);

        Console.WriteLine();

        for (var y = 1; y <= _blizzardHeight; y++)
        {
            for (var x = 1; x <= _blizzardWidth; x++)
            {
                Console.Write(IsOccupied(new Point(x, y), iteration) ? 'X' : '.');
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private int GenerateMoves(int x, int y, Point target, Point origin, int iteration)
    {
        var moveCount = 0;

        // Reached goal (end).
        if (target.Equals(_end) && x == target.X && y == target.Y - 1)
        {
            _possibleMoves[0].X = x;
            _possibleMoves[0].Y = y + 1;

            return 1;
        }

        // Reached goal (start).
        if (target.Equals(_start) && x == target.X && y == target.Y + 1)
        {
            _possibleMoves[0].X = x;
            _possibleMoves[0].Y = y - 1;

            return 1;
        }

        // Loiter.
        if (! IsOccupied(new Point(x, y), iteration))
        {
            _possibleMoves[0].X = x;
            _possibleMoves[0].Y = y;

            moveCount++;
        }

        // In and out of start/end.
        if (origin.Equals(_start))
        {
            if (y == 0 && x == origin.X)
            {
                _possibleMoves[moveCount].X = x;
                _possibleMoves[moveCount].Y = 1;

                moveCount++;

                return moveCount;
            }

            if (y == 1 && x == origin.X)
            {
                _possibleMoves[moveCount].X = x;
                _possibleMoves[moveCount].Y = y - 1;

                moveCount++;
            }
        }
        else
        {
            if (y == _height - 1 && x == origin.X)
            {
                _possibleMoves[moveCount].X = x;
                _possibleMoves[moveCount].Y = y - 2;

                moveCount++;

                return moveCount;
            }

            if (y == _height - 2 && x == origin.X)
            {
                _possibleMoves[moveCount].X = x;
                _possibleMoves[moveCount].Y = y + 1;

                moveCount++;
            }
        }

        // Right?
        if (x < _blizzardWidth && ! IsOccupied(new Point(x + 1, y), iteration))
        {
            _possibleMoves[moveCount].X = x + 1;
            _possibleMoves[moveCount].Y = y;

            moveCount++;
        }

        // Left?
        if (x > 1 && ! IsOccupied(new Point(x - 1, y), iteration))
        {
            _possibleMoves[moveCount].X = x - 1;
            _possibleMoves[moveCount].Y = y;

            moveCount++;
        }

        // Down?
        if (y < _blizzardHeight && ! IsOccupied(new Point(x, y + 1), iteration))
        {
            _possibleMoves[moveCount].X = x;
            _possibleMoves[moveCount].Y = y + 1;

            moveCount++;
        }

        // Up?
        if (y > 1 && ! IsOccupied(new Point(x, y - 1), iteration))
        {
            _possibleMoves[moveCount].X = x;
            _possibleMoves[moveCount].Y = y - 1;

            moveCount++;
        }

        return moveCount;
    }

    private bool IsOccupied(Point position, int iteration)
    {
        var xD = iteration % _blizzardWidth;

        var yD = iteration % _blizzardHeight;

        var found = _initialStorms.Where(s => s.Direction is '>' or '<')
                                  .Any(s => position.X == s.Direction switch
                                            {
                                                '>' => (s.X - 1 + xD) % _blizzardWidth + 1,
                                                '<' => (s.X - 1 + _blizzardWidth - xD) % _blizzardWidth + 1,
                                                _ => throw new PuzzleException("This exception shouldn't happen.")
                                            }
                                            && position.Y == s.Y);

        if (found)
        {
            return true;
        }

        found = _initialStorms.Where(s => s.Direction is 'v' or '^')
                              .Any(s => position.Y == s.Direction switch
                                        {
                                            'v' => (s.Y - 1 + yD) % _blizzardHeight + 1,
                                            '^' => (s.Y - 1 + _blizzardHeight - yD) % _blizzardHeight + 1,
                                            _ => throw new PuzzleException("This exception shouldn't happen.")
                                        }
                                        && position.X == s.X);

        //found = _initialStorms.Any(s => s.X == position.X &&
        //                                (s.Direction == 'v'
        //                                     ? s.Y + yD
        //                                     : s.Y + _blizzardHeight - yD) == position.Y);

        //found = _initialStorms.Any(s => s.X == position.X &&
        //                                (s.Direction == 'v'
        //                                     ? s.Y + yD
        //                                     : 0) == position.Y);
        
        return found;
    }
}