using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._13;

public abstract class Base : Solution
{
    public override string Description => "Crashing carts";

    private int _width;

    private int _height;

    protected char[,] Map;

    protected readonly List<Cart> Carts = new();

    protected readonly IVisualiser<PuzzleState> Visualiser;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        Visualiser = visualiser;
    }

    protected void MoveCarts()
    {
        foreach (var cart in Carts)
        {
            cart.Position.X += cart.Direction.X;

            cart.Position.Y += cart.Direction.Y;

            CheckDirectionChange(cart);
        }
    }

    private void CheckDirectionChange(Cart cart)
    {
        var track = Map[cart.Position.X, cart.Position.Y];

        if (track == '┼')
        {
            cart.LastMove = (Move) (((int) cart.LastMove + 1) % 3);

            if (cart.LastMove == Move.Left)
            {
                cart.Direction = new Point(cart.Direction.Y, -cart.Direction.X);
            }
            else if (cart.LastMove == Move.Right)
            {
                cart.Direction = new Point(-cart.Direction.Y, cart.Direction.X);
            }

            return;
        }

        if (track is '┐' or '└')
        {
            cart.Direction = cart.Direction.Y != 0 
                                 ? new Point(cart.Direction.Y, -cart.Direction.X) 
                                 : new Point(-cart.Direction.Y, cart.Direction.X);

            return;
        }

        if (track is '┘' or '┌')
        {
            cart.Direction = cart.Direction.Y != 0 
                                 ? new Point(-cart.Direction.Y, cart.Direction.X) 
                                 : new Point(cart.Direction.Y, -cart.Direction.X);
        }
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        Map = new char[_width, _height];

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

                Map[x, y] = piece;
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