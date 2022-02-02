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
        var targets = GetTargets().ToList();

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

    private IEnumerable<Unit> GetTargets()
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

        // Find reachable target cells

        // Find nearest of those

        // Apply reading order to those (if > 1)

        return Enumerable.Empty<Unit>();
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

public enum Type
{
    Elf,
    Goblin
}