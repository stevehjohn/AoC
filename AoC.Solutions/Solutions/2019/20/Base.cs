using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._20;

public abstract class Base : Solution
{
    public override string Description => "Doughnut maze*";

    protected int[,] Maze;

    protected int Width;

    protected int Height;

    protected List<(int Id, Point Position)> Portals = new();

    protected Point Start;

    protected Point End;

    protected string TravelMaze(bool recursive = false)
    {
        ParseInput();

#if DEBUG && DUMP
        Console.CursorVisible = false;

        DumpMap();
#endif

        var result = FindShortestRoute(recursive);

#if DEBUG && DUMP
        Console.CursorLeft = 0;

        Console.CursorTop = Height + 2;

        Console.ForegroundColor = ConsoleColor.Green;
#endif

        return result.ToString();
    }

    private int FindShortestRoute(bool recursive)
    {
        var bots = new List<Bot>
                   {
                       new(new Point(Start), new Point(End), Maze, Portals, recursive)
                   };

#if DEBUG && DUMP
        DrawBots(bots, Portals, recursive);
#endif

        while (true)
        {
            var newBots = new List<Bot>();

            foreach (var bot in bots)
            {
                if (bot.IsHome)
                {
#if DEBUG && DUMP
                    DrawHistory(bot);
#endif

                    return bot.Steps;
                }

                newBots.AddRange(bot.Move());
            }

            bots = newBots;

#if DEBUG && DUMP
            DrawBots(bots, Portals, recursive);
#endif
        }
    }

    private void ParseInput()
    {
        Width = Input[2].Length - 2;

        Height = Input.Length - 2;

        Maze = new int[Width, Height];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var c = Input[y + 1][x + 1];

                if (c == ' ')
                {
                    Maze[x, y] = -2;

                    continue;
                }

                if (c == '#')
                {
                    Maze[x, y] = -1;

                    continue;
                }

                if (c == '.')
                {
                    continue;
                }

                var portal = EncodePortal(x + 1, y + 1);

                Maze[x, y] = portal;

                Portals.Add((portal, new Point(x, y)));
            }
        }

        TrimPortals();

        Start = FindPortalExit(Portals.Single(p => p.Id == 27).Position);

        End = FindPortalExit(Portals.Single(p => p.Id == 702).Position);
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

        if (location.X == Width - 1)
        {
            return new Point(location.X - 1, location.Y);
        }

        if (location.Y == Height - 1)
        {
            return new Point(location.X, location.Y - 1);
        }

        if (Maze[location.X, location.Y - 1] == 0)
        {
            return new Point(location.X, location.Y - 1);
        }

        if (Maze[location.X - 1, location.Y] == 0)
        {
            return new Point(location.X - 1, location.Y);
        }

        if (Maze[location.X + 1, location.Y] == 0)
        {
            return new Point(location.X + 1, location.Y);
        }

        if (Maze[location.X, location.Y + 1] == 0)
        {
            return new Point(location.X, location.Y + 1);
        }

        throw new PuzzleException("Unable to find portal exit.");
    }

    private void TrimPortals()
    {
        var toRemove = new Stack<int>();

        for (var i = 0; i < Portals.Count; i++)
        {
            var portal = Portals[i];

            var x = portal.Position.X;

            var y = portal.Position.Y;

            if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
            {
                continue;
            }

            if (Maze[x - 1, y] != 0 && Maze[x, y - 1] != 0 && Maze[x + 1, y] != 0 && Maze[x, y + 1] != 0)
            {
                Maze[x, y] = -2;

                toRemove.Push(i);
            }
        }

        while (toRemove.Count > 0)
        {
            Portals.RemoveAt(toRemove.Pop());
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

#if DEBUG && DUMP
    private static void DrawHistory(Bot bot)
    {
        var previousHistory = new Point();

        for (var i = bot.History.Count - 1; i >= 0; i--)
        {
            var position = bot.History[i];

            Console.CursorLeft = position.X + 1;

            Console.CursorTop = position.Y + 1;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write('█');

            if (previousHistory.X > 0)
            {
                Console.CursorLeft = previousHistory.X + 1;

                Console.CursorTop = previousHistory.Y + 1;

                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.Write('█');
            }

            Thread.Sleep(1);

            previousHistory = new Point(position);
        }
    }

    private List<Point> _previousPositions = new();

    // ReSharper disable StringLiteralTypo
    private string Levels = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    // ReSharper restore StringLiteralTypo

    private void DrawBots(List<Bot> bots, List<(int Id, Point Position)> portals, bool recursive)
    {
        foreach (var portal in portals)
        {
            Console.CursorLeft = 1 + portal.Position.X;

            Console.CursorTop = 1 + portal.Position.Y;

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write('█');
        }

        foreach (var bot in bots)
        {
            Console.CursorLeft = 1 + bot.Position.X;

            Console.CursorTop = 1 + bot.Position.Y;

            if (recursive)
            {
                Console.BackgroundColor = ConsoleColor.Red;

                Console.ForegroundColor = ConsoleColor.White;

                Console.Write(Levels[bot.Level]);

                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write('█');
            }
        }

        foreach (var position in _previousPositions)
        {
            if (portals.Any(p => p.Position.Equals(position)))
            {
                continue;
            }

            Console.CursorLeft = 1 + position.X;

            Console.CursorTop = 1 + position.Y;

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write(recursive ? ' ' : '█');
        }

        Console.ForegroundColor = ConsoleColor.DarkGreen;

        _previousPositions = bots.Select(b => new Point(b.Position)).ToList();
    }

    private void DumpMap()
    {
        Console.Clear();

        Console.CursorLeft = 0;

        Console.CursorTop = 1;

        for (var y = 0; y < Height; y++)
        {
            Console.Write(' ');

            for (var x = 0; x < Width; x++)
            {
                if (Maze[x, y] is -2 or 0)
                {
                    Console.Write(' ');

                    continue;
                }

                if (Maze[x, y] == -1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                    Console.Write('█');

                    continue;
                }

                if (Maze[x, y] > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write('█');
                }
            }

            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.White;

        Console.CursorLeft = End.X + 1;

        Console.CursorTop = End.Y + 1;

        Console.Write('█');

        Console.BackgroundColor = ConsoleColor.Black;
    }
#endif
}