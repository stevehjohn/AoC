using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._13;

[UsedImplicitly]
public class Part1 : Base
{
    private int _width;

    private int _height;

    private char[,] _map;

    private readonly List<Point> _carts = new();

    public override string GetAnswer()
    {
        ParseInput();

        return "TESTING";
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
                    case '<':
                        _carts.Add(new Point(x, y));

                        piece = '─';

                        break;

                    case '^':
                    case 'v':
                        _carts.Add(new Point(x, y));

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
            if (SafeGetInput(x, y - 1) is '|' or '+' && SafeGetInput(x + 1, y) is '-' or '+')
            {
                return '└';
            }

            return '┐';
        }

        if (SafeGetInput(x + 1, y) is '-' or '+' && SafeGetInput(x, y + 1) is '|' or '+')
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