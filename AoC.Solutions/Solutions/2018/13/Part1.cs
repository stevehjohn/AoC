using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._13;

[UsedImplicitly]
public class Part1 : Base
{
    private int _width;

    private int _height;

    private char[,] _map;

    private readonly List<Cart> _carts = new();

    private readonly IVisualiser<PuzzleState> _visualiser;

    public Part1()
    {
    }

    public Part1(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    public override string GetAnswer()
    {
        ParseInput();

        if (_visualiser != null)
        {
            _visualiser.PuzzleStateChanged(new PuzzleState { Map = _map, Carts = _carts.Select(c => new Cart(c)).ToList() });
        }

        while (true)
        {
            MoveCarts();

            if (_visualiser != null)
            {
                _visualiser.PuzzleStateChanged(new PuzzleState { Map = _map, Carts = _carts.Select(c => new Cart(c)).ToList() });
            }
        }

        return "TESTING";
    }

    private void MoveCarts()
    {
        foreach (var cart in _carts)
        {
            cart.Position.X += cart.Direction.X;

            cart.Position.Y += cart.Direction.Y;

            CheckDirectionChange(cart);
        }
    }

    private void CheckDirectionChange(Cart cart)
    {
        var track = _map[cart.Position.X, cart.Position.Y];

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

    private void ParseInput()
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
                        _carts.Add(new Cart(new Point(x, y), new Point(1, 0)));

                        piece = '─';

                        break;

                    case '<':
                        _carts.Add(new Cart(new Point(x, y), new Point(-1, 0)));

                        piece = '─';

                        break;

                    case '^':
                        _carts.Add(new Cart(new Point(x, y), new Point(0, -1)));

                        piece = '│';

                        break;

                    case 'v':
                        _carts.Add(new Cart(new Point(x, y), new Point(0, 1)));

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

public enum Move
{
    Left = 0,
    // ReSharper disable once UnusedMember.Global
    Straight = 1,
    Right = 2
}