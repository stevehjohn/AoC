﻿#define DUMP
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
        var queue = new PriorityQueue<Bot, int>();

        queue.Enqueue(new Bot(new Point(Start), new Point(End), Maze, Portals, recursive), 0);

#if DEBUG && DUMP
        var list = new List<Point>
                   {
                       new (Start)
                   };

        DrawBots(list, recursive);
#endif

        while (queue.Count > 0)
        {
            var bot = queue.Dequeue();

            if (bot.IsHome)
            {
#if DEBUG && DUMP
                DrawHistory(bot);
#endif

                return bot.Steps;
            }

            var move = bot.Move();

            move.ForEach(b => queue.Enqueue(b, int.MaxValue - b.Steps));

#if DEBUG && DUMP
            list.Remove(bot.Position);

            list.AddRange(move.Select(m => m.Position));

            DrawBots(list, recursive);

            Thread.Sleep(1);
#endif
        }

        throw new PuzzleException("No solution found.");
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

            Console.SetCursorPosition(position.X + 1, position.Y + 1);

            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write('█');

            if (previousHistory.X > 0)
            {
                Console.SetCursorPosition(previousHistory.X + 1, previousHistory.Y + 1);

                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.Write('█');
            }

            previousHistory = new Point(position);

            Thread.Sleep(1);
        }
    }

    private List<Point> _previousPositions = new();

    private void DrawBots(List<Point> bots, bool recursive)
    {
        foreach (var bot in bots.Except(_previousPositions))
        {
            if (bot.X < 1 || bot.Y < 1 || bot.X >= Width - 1 || bot.Y >= Height - 1)
            {
                continue;
            }

            Console.SetCursorPosition(1 + bot.X, 1 + bot.Y);
            
            if (recursive)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write('█');

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
            if (bots.Any(b => b.Equals(position)))
            {
                continue;
            }

            Console.SetCursorPosition(1 + position.X, 1 + position.Y);

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write(recursive ? ' ' : '█');
        }

        Console.ForegroundColor = ConsoleColor.DarkGreen;

        _previousPositions = bots.Select(b => new Point(b)).ToList();
    }

    private void DumpMap()
    {
        Console.Clear();

        Console.SetCursorPosition(0, 1);

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
                    if (Math.Abs(x - Start.X) == 1 && y == Start.Y || x == Start.X && Math.Abs(y - Start.Y) == 1 || Math.Abs(x - End.X) == 1 && y == End.Y || x == End.X && Math.Abs(y - End.Y) == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    Console.Write('█');
                }
            }

            Console.WriteLine();
        }

        Console.BackgroundColor = ConsoleColor.Black;
    }
#endif
}