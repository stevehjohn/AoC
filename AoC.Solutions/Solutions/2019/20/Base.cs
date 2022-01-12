using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._20;

public abstract class Base : Solution
{
    public override string Description => "Doughnut maze";

    protected int[,] Maze;

    protected int Width;

    protected int Height;

    protected List<(int Id, Point Position)> Portals = new();

    protected Point Start;

    protected Point End;

    protected void ParseInput()
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

    protected Point FindPortalExit(Point location)
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
            return (Input[y - 1][x] - '@') * 26  + (Input[y][x] - '@');
        }

        if (char.IsLetter(Input[y + 1][x]))
        {
            return (Input[y][x] - '@') * 26  + (Input[y + 1][x] - '@');
        }

        if (char.IsLetter(Input[y][x - 1]))
        {
            return (Input[y][x - 1] - '@') * 26  + (Input[y][x] - '@');
        }

        if (char.IsLetter(Input[y][x + 1]))
        {
            return (Input[y][x] - '@') * 26  + (Input[y][x + 1] - '@');
        }

        throw new PuzzleException("Unable to parse portal.");
    }
}