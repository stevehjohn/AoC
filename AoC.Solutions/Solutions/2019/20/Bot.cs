using System.Security.Principal;
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._20;

public class Bot
{
    public Point Position { get; }

    public int Steps { get; private set; }

    public bool IsHome => Position.Equals(_destination);

    private readonly Point _destination;

    private Point _direction;

    private readonly int[,] _maze;
    
    private List<(int Id, Point Position)> _portals = new();

    public Bot(Point position, Point destination, int[,] map, List<(int Id, Point Position)> portals)
    {
        Position = position;

        _direction = new Point();

        _destination = destination;

        _maze = map;

        _portals = portals;

        Steps = 0;
    }

    protected Bot(Bot bot, Point direction)
    {
        Position = new Point(bot.Position);

        Steps = bot.Steps;

        _maze = bot._maze;

        _direction = direction;

        _destination = bot._destination;

        _portals = bot._portals;

        Position.X += _direction.X;

        Position.Y += _direction.Y;

        Steps++;
    }

    public List<Bot> Move()
    {
        if (_maze[Position.X, Position.Y] > 0)
        {
            var portalId = _portals.Single(p => p.Position.X == Position.X && p.Position.Y == Position.Y).Id;

            var destination = _portals.Single(p => p.Id == portalId && (p.Position.X != Position.X || p.Position.Y != Position.Y));

            Position.X = destination.Position.X;

            Position.Y = destination.Position.Y;

            var move = GetPossibleMoves().Single();

            Position.X += move.X;

            Position.Y += move.Y;

            _direction = move;
        }

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

        if (Position.X > 0 && _maze[Position.X - 1, Position.Y] is > -1 and not 27)
        {
            moves.Add(new Point(-1, 0));
        }

        if (Position.X < _maze.GetLength(0) - 1 && _maze[Position.X + 1, Position.Y] is > -1 and not 27)
        {
            moves.Add(new Point(1, 0));
        }

        if (Position.Y > 0 && _maze[Position.X, Position.Y - 1] is > -1 and not 27)
        {
            moves.Add(new Point(0, -1));
        }

        if (Position.Y < _maze.GetLength(1) - 1 &&_maze[Position.X, Position.Y + 1] is > -1 and not 27)
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