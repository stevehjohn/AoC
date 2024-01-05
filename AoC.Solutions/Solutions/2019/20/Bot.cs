using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._20;

public class Bot
{
    private Point Position { get; }

    public int Steps { get; private set; }

    public bool IsHome => Position.Equals(_destination) && (Level == 0 || ! _recursive);

    public static HashSet<int> DeadEnds = [];

    private readonly Point _destination;

    private Point _direction;

    private readonly int[,] _maze;

    private readonly List<(int Id, Point Position)> _portals;

#if DEBUG && DUMP
    public List<Point> History { get; }
#endif

    private int Level { get; set; }

    private readonly bool _recursive;

    private readonly Dictionary<int, int> _visitedPortals;

    private readonly Point _splitAt;

    public Bot(Point position, Point destination, int[,] map, List<(int Id, Point Position)> portals, bool recursive)
    {
        Position = position;

        _direction = new Point();

        _destination = destination;

        _maze = map;

        _portals = portals;

        _recursive = recursive;

#if DEBUG && DUMP
        History = new List<Point>
                  {
                      new(Position)
                  };
#endif

        _visitedPortals = new Dictionary<int, int>();

        Steps = 0;
    }

    private Bot(Bot bot, Point direction)
    {
        Position = new Point(bot.Position);

        Steps = bot.Steps;

        _maze = bot._maze;

        _direction = direction;

        _destination = bot._destination;

        _portals = bot._portals;

        Position.X += _direction.X;

        Position.Y += _direction.Y;

        _splitAt = new Point(Position);

        _recursive = bot._recursive;

#if DEBUG && DUMP
        History = bot.History.ToList();

        History.Add(new Point(Position.X, Position.Y));
#endif

        Level = bot.Level;

        _visitedPortals = new Dictionary<int, int>(bot._visitedPortals);

        Steps++;
    }

    public List<Bot> Move()
    {
        var moves = _recursive
                        ? GetPossibleMovesWhenRecursive()
                        : GetPossibleMoves();

        var bots = new List<Bot>();

        if (DeadEnds.Contains(Position.GetHashCode()))
        {
            return bots;
        }

        switch (moves.Count)
        {
            case 0 when _splitAt != null:
                DeadEnds.Add(Position.GetHashCode());

                return bots;
            case 1:
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
                        Level--;

                        if (Level < 0)
                        {
                            return bots;
                        }
                    }
                    else
                    {
                        if (! _visitedPortals.TryAdd(portal.Id, 0))
                        {
                            if (_visitedPortals[portal.Id] > 1)
                            {
                                return bots;
                            }

                            _visitedPortals[portal.Id]++;
                        }

                        Level++;

                        if (Level > _portals.Count / 2)
                        {
                            return bots;
                        }
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

#if DEBUG && DUMP
                History.Add(new Point(Position.X, Position.Y));
#endif

                    _direction = move;
                }
                else
                {
#if DEBUG && DUMP
                History.Add(new Point(Position));
#endif
                }

                bots.Add(this);

                return bots;
            }
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
        if (Level == 0)
        {
            // TODO: Disable outer layer teleports.
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