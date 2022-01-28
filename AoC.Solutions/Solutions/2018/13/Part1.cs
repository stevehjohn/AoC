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

    private readonly List<(Point Position, Point Direction)> _carts = new();

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
            _visualiser.PuzzleStateChanged(new PuzzleState { Map = _map, Carts = _carts });
        }

        while (true)
        {
            MoveCarts();

            if (_visualiser != null)
            {
                _visualiser.PuzzleStateChanged(new PuzzleState { Map = _map, Carts = _carts });
            }
        }

        return "TESTING";
    }

    private void MoveCarts()
    {
        foreach (var cart in _carts)
        {
            cart.Position.X += cart.Direction.X;

            cart.Position.Y += cart.Position.Y;
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
                        _carts.Add((new Point(x, y), new Point(1, 0)));

                        piece = '─';

                        break;

                    case '<':
                        _carts.Add((new Point(x, y), new Point(-1, 0)));

                        piece = '─';

                        break;

                    case '^':
                        _carts.Add((new Point(x, y), new Point(0, -1)));

                        piece = '│';

                        break;

                    case 'v':
                        _carts.Add((new Point(x, y), new Point(0, 1)));

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

    // ┐ └ ┼ ┘ ┌ ─ │
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