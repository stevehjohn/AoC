﻿using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._20;

public class Bot
{
    public Point Position { get; }

    public int Steps { get; private set; }

    public bool IsHome => Position.Equals(_destination);

    private readonly Point _destination;

    private Point _direction;

    private readonly int[,] _maze;

    public Bot(Point position, Point destination, int[,] map)
    {
        Position = position;

        _direction = new Point();

        _destination = destination;

        _maze = map;

        Steps = 0;
    }

    protected Bot(Bot bot, Point direction)
    {
        Position = new Point(bot.Position);

        Steps = bot.Steps;

        _maze = bot._maze;

        _direction = direction;

        _destination = bot._destination;

        Position.X += _direction.X;

        Position.Y += _direction.Y;

        Steps++;
    }

    public List<Bot> Move()
    {
        var moves = GetPossibleMoves();

        var bots = new List<Bot>();

        if (moves.Count == 0)
        {
            return bots;
        }

        if (moves.Count == 1)
        {
            _direction = moves.First();

            Position.X += _direction.X;

            Position.Y += _direction.Y;

            Steps++;

            bots.Add(this);

            return bots;
        }

        foreach (var move in moves)
        {
            bots.Add(new Bot(this, move));
        }

        return bots;
    }

    private List<Point> GetPossibleMoves()
    {
        var moves = new List<Point>();

        if (_maze[Position.X - 1, Position.Y] is > -1 and not 27)
        {
            moves.Add(new Point(-1, 0));
        }

        if (_maze[Position.X + 1, Position.Y] is > -1 and not 27)
        {
            moves.Add(new Point(1, 0));
        }

        if (_maze[Position.X, Position.Y - 1] is > -1 and not 27)
        {
            moves.Add(new Point(0, -1));
        }

        if (_maze[Position.X, Position.Y + 1] is > -1 and not 27)
        {
            moves.Add(new Point(0, 1));
        }

        if (_direction.X == 0 && _direction.Y == 0)
        {
            return moves;
        }

        return moves.Where(m => m.X != -_direction.X || m.Y != -_direction.Y).ToList();
    }
}