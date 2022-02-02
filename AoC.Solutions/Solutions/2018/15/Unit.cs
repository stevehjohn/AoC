using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2018._15;

public class Unit
{
    public Type Type { get; }

    public Point Position { get; set; }

    public int Health { get; set; } = 200;

    private bool[,] _map;

    private List<Unit> _otherUnits;

    public Unit(Type type, Point position, bool[,] map)
    {
        Type = type;

        Position = position;

        _map = map;
    }

    public void SetUnits(List<Unit> units)
    {
        _otherUnits = units.Where(u => u != this).ToList();
    }

    public void Play()
    {
        var targets = _otherUnits.Where(u => u.Type != Type).ToList();

        if (targets.Count == 0)
        {
            return;
        }

        var targetCells = GetTargetCells(targets);
    }

    private List<Point> GetTargetCells(List<Unit> targets)
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

    private IEnumerable<Point> CheckNeighborCell(Point position, List<Unit> targets)
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