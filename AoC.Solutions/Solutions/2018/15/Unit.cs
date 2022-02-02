using AoC.Solutions.Common;

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

        var targetCells = GetTargetCells(targets);

        var distances = targetCells.Select(t => (Distance: Math.Abs(t.X - Position.X) + Math.Abs(t.Y - Position.Y), Position: t)).OrderBy(t => t.Distance);

        var reachable = new List<(int Distance, Point Position)>();

        foreach (var (distance, position) in distances)
        {
            if (IsReachable(position))
            {
                reachable.Add((distance, position));
            }

            if (reachable.DistinctBy(t => t.Distance).Count() > 1)
            {
                break;
            }
        }

        var nearestDistance = reachable.MinBy(t => t.Distance);

        reachable = reachable.Where(t => t.Distance == nearestDistance.Distance).OrderBy(t => t.Position.Y).ThenBy(t => t.Position.X).ToList();

        // Move

        adjacent = targets.Where(t => t.Position.X == Position.X && Math.Abs(t.Position.Y - Position.Y) == 1
                                      || Math.Abs(t.Position.X - Position.X) == 1 && t.Position.Y == Position.Y).ToList();

        return adjacent;
    }

    private bool IsReachable(Point position)
    {
        var explorer = new Explorer(_map, _units);

        return explorer.CanReach(Position, position);
    }

    private IEnumerable<Point> GetTargetCells(List<Unit> targets)
    {
        var targetCells = new List<Point>();

        foreach (var target in targets)
        {
            targetCells.AddRange(CheckNeighborCell(new Point(target.Position.X, target.Position.Y - 1), targets));

            targetCells.AddRange(CheckNeighborCell(new Point(target.Position.X + 1, target.Position.Y), targets));

            targetCells.AddRange(CheckNeighborCell(new Point(target.Position.X, target.Position.Y + 1), targets));

            targetCells.AddRange(CheckNeighborCell(new Point(target.Position.X - 1, target.Position.Y), targets));
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