using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2018._13;

public class Cart
{
    private static int _nextCartId = 1;

    public int Id { get; }

    public Point Position { get; }

    public Point Direction { get; set; }

    public Move LastMove { get; set; }

    public Cart(Point position, Point direction)
    {
        Position = new Point(position);

        Direction = new Point(direction);

        LastMove = Move.Right;

        Id = _nextCartId++;
    }

    public Cart(Cart cart)
    {
        Position = new Point(cart.Position);

        Direction = new Point(cart.Direction);

        LastMove = cart.LastMove;

        Id = cart.Id;
    }
}