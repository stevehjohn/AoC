using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public class Bot
{
    private char Name { get; }

    private Point Position { get; }

    private int Steps { get; set; }

    private Point _direction;

    private readonly char[,] _map;

    private readonly Dictionary<string, int> _distances;

    private readonly List<(char Item, int Steps)> _itemHistory;

    private readonly Dictionary<string, List<Point>> _paths;

    private readonly Dictionary<string, HashSet<char>> _doors;

    private readonly List<Point> _positionsSinceLastItem;

    private static readonly HashSet<int> AllHistory = [];

    public Bot(char name, Point position, char[,] map, Dictionary<string, int> distances, Dictionary<string, List<Point>> paths, Dictionary<string, HashSet<char>> doors)
    {
        Name = name;

        Position = new Point(position);

        _direction = new Point();

        _map = map;

        _distances = distances;

        _paths = paths;

        _doors = doors;

        _itemHistory = [(Item: _map[Position.X, Position.Y], Steps: 0)];

        AllHistory.Add(HashCode.Combine(Name, Position));

        _positionsSinceLastItem = [new(Position)];

        Steps = 0;
    }

    private Bot(Bot bot, Point direction)
    {
        Name = bot.Name;

        Position = new Point(bot.Position);

        Steps = bot.Steps;

        _map = bot._map;

        _distances = bot._distances;

        _paths = bot._paths;

        _doors = bot._doors;

        _direction = direction;

        _itemHistory = bot._itemHistory.ToList();

        Position.X += _direction.X;

        Position.Y += _direction.Y;

        AllHistory.Add(HashCode.Combine(Name, Position));

        _positionsSinceLastItem = bot._positionsSinceLastItem.ToList();

        _positionsSinceLastItem.Add(new Point(Position));

        Steps++;

        AddItemHistory();
    }

    public List<Bot> Move()
    {
        var moves = GetPossibleMoves();

        var bots = new List<Bot>();

        if (moves.Count == 0)
        {
            AddBlockerData();

            return bots;
        }

        if (moves.Count == 1)
        {
            if (moves.Count == 1)
            {
                _direction = moves.First();
            }

            Position.X += _direction.X;

            Position.Y += _direction.Y;

            AllHistory.Add(HashCode.Combine(Name, Position));

            _positionsSinceLastItem.Add(new Point(Position));

            Steps++;

            AddItemHistory();

            bots.Add(this);

            return bots;
        }

        foreach (var move in moves)
        {
            var newMove = new Point(Position.X + move.X, Position.Y + move.Y);

            if (AllHistory.Contains(HashCode.Combine(Name, newMove)))
            {
                continue;
            }

            bots.Add(new Bot(this, move));
        }

        return bots;
    }

    public static void ResetStaticHistory()
    {
        AllHistory.Clear();
    }

    private void AddItemHistory()
    {
        var c = _map[Position.X, Position.Y];

        if (char.IsLetter(c) || c == '@' || char.IsNumber(c))
        {
            _itemHistory.Add((Item: c, Steps));

            foreach (var i in _itemHistory)
            {
                var currentSteps = Steps - i.Steps;

                if (i.Item != c)
                {
                    var pair = new string(new[] { i.Item, c }.OrderBy(x => x).ToArray());

                    if (! _distances.TryAdd(pair, currentSteps))
                    {
                        if (_distances[pair] > currentSteps)
                        {
                            _distances[pair] = currentSteps;

                            _paths[pair] = _positionsSinceLastItem.ToList();
                        }
                    }
                    else
                    {
                        _paths.Add(pair, _positionsSinceLastItem.ToList());
                    }
                }
            }
        }
    }

    private void AddBlockerData()
    {
        var historyLength = _itemHistory.Count;

        if (historyLength > 2)
        {
            var first = _itemHistory[0].Item;

            if (char.IsUpper(first))
            {
                return;
            }

            for (var i = 2; i < historyLength; i++)
            {
                var current = _itemHistory[i].Item;

                var pair = new string(new[] { first, current }.OrderBy(x => x).ToArray());

                HashSet<char> doors;

                if (_doors.TryGetValue(pair, out var door))
                {
                    doors = door;
                }
                else
                {
                    doors = [];

                    _doors.Add(pair, doors);
                }

                for (var d = 1; d < i; d++)
                {
                    doors.Add(_itemHistory[d].Item);
                }
            }
        }
    }

    private List<Point> GetPossibleMoves()
    {
        var moves = new List<Point>();

        if (_map[Position.X - 1, Position.Y] != '#')
        {
            moves.Add(new Point(-1, 0));
        }

        if (_map[Position.X + 1, Position.Y] != '#')
        {
            moves.Add(new Point(1, 0));
        }

        if (_map[Position.X, Position.Y - 1] != '#')
        {
            moves.Add(new Point(0, -1));
        }

        if (_map[Position.X, Position.Y + 1] != '#')
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