﻿using System.ComponentModel.DataAnnotations;
using System.Text;
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public class Bot
{
    public char Name { get; }

    public Point Position { get; }

    public int Steps { get; private set; }

    private Point _direction;

    private readonly char[,] _map;

    private readonly Dictionary<string, int> _distances;

    private readonly List<(char Item, int Steps)> _itemHistory;

    private readonly Dictionary<string, List<Point>> _paths;

    private readonly Dictionary<string, string> _doors;
    
    private readonly List<Point> _positionsSinceLastItem;

    private static readonly HashSet<int> AllHistory = new();

    public Bot(char name, Point position, char[,] map, Dictionary<string, int> distances, Dictionary<string, List<Point>> paths, Dictionary<string, string> doors)
    {
        Name = name;

        Position = new Point(position);

        _direction = new Point();

        _map = map;

        _distances = distances;

        _paths = paths;

        _doors = doors;

        _itemHistory = new List<(char Item, int Steps)>
                       {
                           ( Item: _map[Position.X, Position.Y], Steps: 0 )
                       };

        AllHistory.Add(HashCode.Combine(Name, Position));

        _positionsSinceLastItem = new List<Point>
                                  {
                                      new(Position)
                                  };

        Steps = 0;
    }

    protected Bot(Bot bot, Point direction)
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

    private void AddItemHistory()
    {
        var c = _map[Position.X, Position.Y];

        if (char.IsLetter(c) || c == '@')
        {
            _itemHistory.Add((Item: c, Steps));

            foreach (var i in _itemHistory)
            {
                var currentSteps = Steps - i.Steps;

                if (i.Item != c)
                {
                    var pair = new string(new[] { i.Item, c }.OrderBy(x => x).ToArray());

                    if (_distances.ContainsKey(pair))
                    {
                        if (_distances[pair] > currentSteps)
                        {
                            _distances[pair] = currentSteps;

                            _paths[pair] = _positionsSinceLastItem.ToList();

                            AddBlockerData();
                        }
                    }
                    else
                    {
                        _distances.Add(pair, currentSteps);

                        _paths.Add(pair, _positionsSinceLastItem.ToList());

                        AddBlockerData();
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
            var last = _itemHistory[^1].Item;

            if (! char.IsLower(last))
            {
                return;
            }

            var index = 2;

            var blockerBuilder = new StringBuilder();

            while (index < historyLength && char.IsUpper(_itemHistory[^index].Item))
            {
                blockerBuilder.Append(_itemHistory[^index].Item);

                index++;
            }

            var blockers = blockerBuilder.ToString();

            if (char.IsUpper(_itemHistory[^index].Item))
            {
                return;
            }

            var path = $"{_itemHistory[^index].Item}{last}";

            if (_doors.ContainsKey(path))
            {
                _doors[path] = blockers;
            }
            else
            {
                _doors.Add(path, blockers);
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