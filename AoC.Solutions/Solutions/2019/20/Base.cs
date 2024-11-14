using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._20;

public abstract class Base : Solution
{
    public override string Description => "Doughnut maze";

    private int[,] _maze;

    private int _width;

    private int _height;

    private readonly List<(int Id, Point Position)> _portals = [];

    private Point _start;

    private Point _end;

    protected string TravelMaze(bool recursive = false)
    {
        Bot.DeadEnds = [];

        ParseInput();

        var result = FindShortestRoute(recursive);

        return result.ToString();
    }

    private int FindShortestRoute(bool recursive)
    {
        var queue = new PriorityQueue<Bot, int>();

        queue.Enqueue(new Bot(new Point(_start), new Point(_end), _maze, _portals, recursive), 0);

        while (queue.Count > 0)
        {
            var bot = queue.Dequeue();

            if (bot.IsHome)
            {
                return bot.Steps;
            }

            var move = bot.Move();

            move.ForEach(b => queue.Enqueue(b, int.MaxValue - b.Steps));
        }

        throw new PuzzleException("No solution found.");
    }

    private void ParseInput()
    {
        _width = Input[2].Length - 2;

        _height = Input.Length - 2;

        _maze = new int[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var c = Input[y + 1][x + 1];

                switch (c)
                {
                    case ' ':
                        _maze[x, y] = -2;

                        continue;
                    case '#':
                        _maze[x, y] = -1;

                        continue;
                    case '.':
                        continue;
                }

                var portal = EncodePortal(x + 1, y + 1);

                _maze[x, y] = portal;

                _portals.Add((portal, new Point(x, y)));
            }
        }

        TrimPortals();

        _start = FindPortalExit(_portals.Single(p => p.Id == 27).Position);

        _end = FindPortalExit(_portals.Single(p => p.Id == 702).Position);
    }

    private Point FindPortalExit(Point location)
    {
        if (location.X == 0)
        {
            return new Point(location.X + 1, location.Y);
        }

        if (location.Y == 0)
        {
            return new Point(location.X, location.Y + 1);
        }

        if (location.X == _width - 1)
        {
            return new Point(location.X - 1, location.Y);
        }

        if (location.Y == _height - 1)
        {
            return new Point(location.X, location.Y - 1);
        }

        if (_maze[location.X, location.Y - 1] == 0)
        {
            return new Point(location.X, location.Y - 1);
        }

        if (_maze[location.X - 1, location.Y] == 0)
        {
            return new Point(location.X - 1, location.Y);
        }

        if (_maze[location.X + 1, location.Y] == 0)
        {
            return new Point(location.X + 1, location.Y);
        }

        if (_maze[location.X, location.Y + 1] == 0)
        {
            return new Point(location.X, location.Y + 1);
        }

        throw new PuzzleException("Unable to find portal exit.");
    }

    private void TrimPortals()
    {
        var toRemove = new Stack<int>();

        for (var i = 0; i < _portals.Count; i++)
        {
            var portal = _portals[i];

            var x = portal.Position.X;

            var y = portal.Position.Y;

            if (x == 0 || y == 0 || x == _width - 1 || y == _height - 1)
            {
                continue;
            }

            if (_maze[x - 1, y] != 0 && _maze[x, y - 1] != 0 && _maze[x + 1, y] != 0 && _maze[x, y + 1] != 0)
            {
                _maze[x, y] = -2;

                toRemove.Push(i);
            }
        }

        while (toRemove.Count > 0)
        {
            _portals.RemoveAt(toRemove.Pop());
        }
    }

    private int EncodePortal(int x, int y)
    {
        if (char.IsLetter(Input[y - 1][x]))
        {
            return (Input[y - 1][x] - '@') * 26 + (Input[y][x] - '@');
        }

        if (char.IsLetter(Input[y + 1][x]))
        {
            return (Input[y][x] - '@') * 26 + (Input[y + 1][x] - '@');
        }

        if (char.IsLetter(Input[y][x - 1]))
        {
            return (Input[y][x - 1] - '@') * 26 + (Input[y][x] - '@');
        }

        if (char.IsLetter(Input[y][x + 1]))
        {
            return (Input[y][x] - '@') * 26 + (Input[y][x + 1] - '@');
        }

        throw new PuzzleException("Unable to parse portal.");
    }
}