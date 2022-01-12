using System.Security.Principal;
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._20;

public class Bot
{
    public Point Position { get; }

    public int Steps { get; private set; }

    public bool IsHome => Position.Equals(_destination) && _level == 0;

    private readonly Point _destination;

    private Point _direction;

    private readonly int[,] _maze;

    private readonly List<(int Id, Point Position)> _portals;

    public List<Point> History { get; }

    private int _level;

    private bool _recursive;

    public Bot(Point position, Point destination, int[,] map, List<(int Id, Point Position)> portals, bool recursive)
    {
        Position = position;

        _direction = new Point();

        _destination = destination;

        _maze = map;

        _portals = portals;

        History = new List<Point>
                  {
                      new(Position)
                  };

        _recursive = recursive;

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

        History = bot.History.ToList();

        History.Add(new Point(Position.X, Position.Y));

        _level = bot._level;

        _recursive = bot._recursive;

        Steps++;
    }

    public List<Bot> Move()
    {
        var moves = _recursive
                        ? GetPossibleMovesWhenRecursive()
                        : GetPossibleMoves();

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

            if (_maze[Position.X, Position.Y] > 0)
            {
                var portal = _portals.Single(p => p.Position.X == Position.X && p.Position.Y == Position.Y);

                if (portal.Position.X == 0 || portal.Position.Y == 0 || portal.Position.X == _maze.GetLength(0) - 1 || portal.Position.Y == _maze.GetLength(1) - 1)
                {
                    _level--;

                    if (_level < 0)
                    {
                        return bots;
                    }
                }
                else
                {
                    _level++;
                }

                var destination = _portals.Single(p => p.Id == portal.Id && (p.Position.X != Position.X || p.Position.Y != Position.Y));

                Position.X = destination.Position.X;

                Position.Y = destination.Position.Y;

                _direction = new Point(0, 0);

                var move = (_recursive
                                ? GetPossibleMovesWhenRecursive()
                                : GetPossibleMoves()).Single();

                Position.X += move.X;

                Position.Y += move.Y;

                History.Add(new Point(Position.X, Position.Y));

                _direction = move;
            }
            else
            {
                History.Add(new Point(Position));
            }

            bots.Add(this);

            return bots;
        }

        foreach (var move in moves)
        {
            bots.Add(new Bot(this, move));
        }

        return bots;
    }

    // TODO: Could these be combined without losing too much efficiency?
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

        if (Position.Y < _maze.GetLength(1) - 1 && _maze[Position.X, Position.Y + 1] is > -1 and not 27)
        {
            moves.Add(new Point(0, 1));
        }

        if (_direction.X == 0 && _direction.Y == 0)
        {
            return moves;
        }

        return moves.Where(m => m.X != -_direction.X || m.Y != -_direction.Y).ToList();
    }

    private List<Point> GetPossibleMovesWhenRecursive()
    {
        var moves = new List<Point>();

        // TODO: There must be a more elegant way to do this.
        if (_level == 0)
        {
            // TODO: Disable outer layer teleports.
            if (Position.X > 0 && _maze[Position.X - 1, Position.Y] is 0 and not 27)
            {
                moves.Add(new Point(-1, 0));
            }

            if (Position.X < _maze.GetLength(0) - 1 && _maze[Position.X + 1, Position.Y] is 0 and not 27)
            {
                moves.Add(new Point(1, 0));
            }

            if (Position.Y > 0 && _maze[Position.X, Position.Y - 1] is 0 and not 27)
            {
                moves.Add(new Point(0, -1));
            }

            if (Position.Y < _maze.GetLength(1) - 1 && _maze[Position.X, Position.Y + 1] is 0 and not 27)
            {
                moves.Add(new Point(0, 1));
            }
        }
        else
        {
            if (Position.X > 0 && _maze[Position.X - 1, Position.Y] is > -1 and not 27 and not 702)
            {
                moves.Add(new Point(-1, 0));
            }

            if (Position.X < _maze.GetLength(0) - 1 && _maze[Position.X + 1, Position.Y] is > -1 and not 27 and not 702)
            {
                moves.Add(new Point(1, 0));
            }

            if (Position.Y > 0 && _maze[Position.X, Position.Y - 1] is > -1 and not 27 and not 702)
            {
                moves.Add(new Point(0, -1));
            }

            if (Position.Y < _maze.GetLength(1) - 1 && _maze[Position.X, Position.Y + 1] is > -1 and not 27 and not 702)
            {
                moves.Add(new Point(0, 1));
            }
        }

        if (_direction.X == 0 && _direction.Y == 0)
        {
            return moves;
        }

        return moves.Where(m => m.X != -_direction.X || m.Y != -_direction.Y).ToList();
    }
}