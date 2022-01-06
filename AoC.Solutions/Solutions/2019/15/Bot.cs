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

    // TODO: Refactor this a bit to remove duplicate code.
    public List<Bot> Move()
    {
        var moves = GetPossibleMoves();

        if (_direction.X == 0 && _direction.Y == 0)
        {
            _direction = moves.Single();
        } 
        else if (moves.Count == 2)
        {
            if (! moves.Any(m => m.Equals(_direction)))
            {
                _direction = moves.Single(m => m.X != -_direction.X && m.Y != -_direction.Y);
            }
        }

        Position.X += _direction.X;

        Position.Y += _direction.Y;

        Steps++;

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

        return moves;
    }
}