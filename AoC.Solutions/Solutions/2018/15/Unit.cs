﻿using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2018._15;

public class Unit
{
    public Type Type { get; }

    public Point Position { get; set; }

    public int Health { get; set; } = 200;

    private readonly bool[,] _map;

    private readonly List<Unit> _units;

    public Unit(Type type, Point position, bool[,] map, List<Unit> units)
    {
        Type = type;

        Position = position;

        _map = map;

        _units = units;
    }

    public void Play()
    {
        var targets = Move().ToList();

        if (targets.Any())
        {
            Attack(targets);
        }
    }

    private void Attack(IEnumerable<Unit> targets)
    {
        var target = targets.OrderBy(u => u.Health).ThenBy(u => u.Position.Y).ThenBy(u => u.Position.X).First();

        target.Health -= 3;

        if (target.Health < 1)
        {
            _units.Remove(target);
        }
    }

    private IEnumerable<Unit> Move()
    {
        var targets = _units.Where(u => u.Type != Type && u != this).ToList();

        if (targets.Count == 0)
        {
            return Enumerable.Empty<Unit>();
        }

        var adjacent = targets.Where(t => t.Position.X == Position.X && Math.Abs(t.Position.Y - Position.Y) == 1
                                          || Math.Abs(t.Position.X - Position.X) == 1 && t.Position.Y == Position.Y).ToList();

        if (adjacent.Count > 0)
        {
            return adjacent;
        }

        var targetCells = GetTargetCells(targets).Distinct();

        var paths = IsReachable(targetCells.ToList());

        if (paths.Count == 0)
        {
            return Enumerable.Empty<Unit>();
        }

        var movesOrdered = paths.OrderBy(p => p.Count).ThenBy(p => p.Skip(1).First().Y).ThenBy(p => p.Skip(1).First().X);

        Position = movesOrdered.First().Skip(1).First();

        adjacent = targets.Where(t => t.Position.X == Position.X && Math.Abs(t.Position.Y - Position.Y) == 1
                                      || Math.Abs(t.Position.X - Position.X) == 1 && t.Position.Y == Position.Y).ToList();

        return adjacent;
    }

    private List<List<Point>> IsReachable(List<Point> targets)
    {
        var queue = new Queue<List<Point>>();

        queue.Enqueue(new List<Point> { Position });

        var visited = new List<Point> { Position };

        var paths = new List<List<Point>>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            var point = current[^1];

            if (targets.Any(t => t.Equals(point)))
            {
                paths.Add(current);

                continue;
            }

            var newPoint = new Point(point.X, point.Y - 1);

            if (! _map[newPoint.X, newPoint.Y] && ! visited.Any(v => v.Equals(newPoint)) && ! _units.Any(u => u.Position.Equals(newPoint)))
            {
                visited.Add(newPoint);

                queue.Enqueue(new List<Point>(current) { newPoint });
            }

            newPoint = new Point(point.X - 1, point.Y);

            if (! _map[newPoint.X, newPoint.Y] && ! visited.Any(v => v.Equals(newPoint)) && ! _units.Any(u => u.Position.Equals(newPoint)))
            {
                visited.Add(newPoint);

                queue.Enqueue(new List<Point>(current) { newPoint });
            }

            newPoint = new Point(point.X + 1, point.Y);

            if (! _map[newPoint.X, newPoint.Y] && ! visited.Any(v => v.Equals(newPoint)) && ! _units.Any(u => u.Position.Equals(newPoint)))
            {
                visited.Add(newPoint);

                queue.Enqueue(new List<Point>(current) { newPoint });
            }

            newPoint = new Point(point.X, point.Y + 1);

            if (! _map[newPoint.X, newPoint.Y] && ! visited.Any(v => v.Equals(newPoint)) && ! _units.Any(u => u.Position.Equals(newPoint)))
            {
                visited.Add(newPoint);

                queue.Enqueue(new List<Point>(current) { newPoint });
            }
        }

        return paths;
    }

    private IEnumerable<Point> GetTargetCells(List<Unit> targets)
    {
        var targetCells = new List<Point>();

        foreach (var target in targets)
        {
            targetCells.AddRange(CheckNeighborCell(new Point(target.Position.X, target.Position.Y - 1), targets));

            targetCells.AddRange(CheckNeighborCell(new Point(target.Position.X - 1, target.Position.Y), targets));

            targetCells.AddRange(CheckNeighborCell(new Point(target.Position.X + 1, target.Position.Y), targets));

            targetCells.AddRange(CheckNeighborCell(new Point(target.Position.X, target.Position.Y + 1), targets));
        }

        return targetCells;
    }

    private IEnumerable<Point> CheckNeighborCell(Point position, IEnumerable<Unit> targets)
    {
        if (_map[position.X, position.Y] || targets.Any(t => t.Position.Equals(position)))
        {
            return Enumerable.Empty<Point>();
        }

        return new List<Point> { position };
    }
}