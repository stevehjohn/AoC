using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2018._13;

public class Cart
{
    public Point Position { get; set; }

    public Point Direction { get; set; }

    public Move LastMove { get; set; }

    public Cart(Point position, Point direction)
    {
        Position = new Point(position);

        Direction = new Point(direction);

        LastMove = Move.Right;
    }

    public Cart(Cart cart)
    {
        Position = new Point(cart.Position);

        Direction = new Point(cart.Direction);

        LastMove = cart.LastMove;
    }
}