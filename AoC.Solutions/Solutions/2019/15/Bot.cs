using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._15;

public class Bot
{
    public Point Position { get; }

    public int Steps { get; private set; }

    public bool IsHome => Position.Equals(_destination);

    private readonly Point _destination;

    private Point _direction;

    private readonly bool[,] _map;

    public Bot(Point position, Point destination, bool[,] map)
    {
        Position = position;

        _direction = new Point();

        _destination = destination;

        _map = map;

        Steps = 0;
    }

    public List<Bot> Move()
    {
        var moves = GetPossibleMoves();

        if (moves.Count == 0)
        {
            return null;
        }

        var bots = new List<Bot>();

        if (moves.Count == 1)
        {
            _direction = moves.First();

            Position.X += _direction.X;

            Position.Y += _direction.Y;

            Steps++;

            bots.Add(this);

            return bots;
        }

        return null;
    }

    private List<Point> GetPossibleMoves()
    {
        var moves = new List<Point>();

        if (_map[Position.X - 1, Position.Y])
        {
            moves.Add(new Point(-1, 0));
        }

        if (_map[Position.X + 1, Position.Y])
        {
            moves.Add(new Point(1, 0));
        }

        if (_map[Position.X, Position.Y - 1])
        {
            moves.Add(new Point(0, -1));
        }

        if (_map[Position.X, Position.Y + 1])
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