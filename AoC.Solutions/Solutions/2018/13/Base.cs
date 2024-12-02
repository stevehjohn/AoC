using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._13;

public abstract class Base : Solution
{
    public override string Description => "Crashing carts";

    private int _width;

    private int _height;

    private char[,] _map;

    protected readonly List<Cart> Carts = [];

    private readonly IVisualiser<PuzzleState> _visualiser;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    protected void Visualise(Point collisionPoint = null, bool isFinalState = false)
    {
        _visualiser?.PuzzleStateChanged(new PuzzleState { Map = _map, Carts = Carts.Select(c => new Cart(c)).ToList(), CollisionPoint = collisionPoint, IsFinalState = isFinalState });
    }

    protected void EndVisualisation()
    {
        _visualiser?.PuzzleComplete();
    }

    protected Point MoveCarts()
    {
        Point collision = null;

        foreach (var cart in Carts.OrderBy(c => c.Position.Y).ThenBy(c => c.Position.X))
        {
            cart.Position.X += cart.Direction.X;

            cart.Position.Y += cart.Direction.Y;

            CheckDirectionChange(cart);

            collision ??= CheckForCollision();
        }

        return collision;
    }

    private Point CheckForCollision()
    {
        foreach (var cart in Carts)
        {
            foreach (var other in Carts)
            {
                if (cart == other)
                {
                    continue;
                }

                if (cart.Position.Equals(other.Position))
                {
                    Carts.Remove(cart);

                    Carts.Remove(other);

                    return cart.Position;
                }
            }
        }

        return null;
    }

    private void CheckDirectionChange(Cart cart)
    {
        var track = _map[cart.Position.X, cart.Position.Y];

        switch (track)
        {
            case '┼':
            {
                cart.LastMove = (Move) (((int) cart.LastMove + 1) % 3);

                cart.Direction = cart.LastMove switch
                {
                    Move.Left => new Point(cart.Direction.Y, -cart.Direction.X),
                    Move.Right => new Point(-cart.Direction.Y, cart.Direction.X),
                    _ => cart.Direction
                };

                return;
            }
            case '┐' or '└':
                cart.Direction = cart.Direction.Y != 0
                    ? new Point(cart.Direction.Y, -cart.Direction.X)
                    : new Point(-cart.Direction.Y, cart.Direction.X);

                return;
            case '┘' or '┌':
                cart.Direction = cart.Direction.Y != 0
                    ? new Point(-cart.Direction.Y, cart.Direction.X)
                    : new Point(cart.Direction.Y, -cart.Direction.X);
                break;
        }
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                char piece;

                switch (Input[y][x])
                {
                    case '>':
                        Carts.Add(new Cart(new Point(x, y), new Point(1, 0)));

                        piece = '─';

                        break;

                    case '<':
                        Carts.Add(new Cart(new Point(x, y), new Point(-1, 0)));

                        piece = '─';

                        break;

                    case '^':
                        Carts.Add(new Cart(new Point(x, y), new Point(0, -1)));

                        piece = '│';

                        break;

                    case 'v':
                        Carts.Add(new Cart(new Point(x, y), new Point(0, 1)));

                        piece = '│';

                        break;

                    case '-':
                        piece = '─';

                        break;

                    case '|':
                        piece = '│';

                        break;

                    case '+':
                        piece = '┼';

                        break;

                    case '\\':
                    case '/':
                        piece = DetermineCornerType(Input[y][x], x, y);

                        break;

                    default:
                        piece = ' ';

                        break;
                }

                _map[x, y] = piece;
            }
        }
    }

    private char DetermineCornerType(char piece, int x, int y)
    {
        if (piece == '\\')
        {
            if (SafeGetInput(x, y - 1) is '|' or '+' or '^' or 'v' && SafeGetInput(x + 1, y) is '-' or '+' or '<' or '>')
            {
                return '└';
            }

            return '┐';
        }

        if (SafeGetInput(x + 1, y) is '-' or '+' or '<' or '>' && SafeGetInput(x, y + 1) is '|' or '+' or '^' or 'v')
        {
            return '┌';
        }

        return '┘';
    }

    private char SafeGetInput(int x, int y)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height)
        {
            return '\0';
        }

        return Input[y][x];
    }
}