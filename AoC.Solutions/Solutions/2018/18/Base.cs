using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._18;

public abstract class Base : Solution
{
    public override string Description => "I'm a lumberjack...";

    private const int Width = 50;

    private const int Height = 50;

    private char[,] _map;

    protected int GetResourceValue()
    {
        var wood = 0;

        var yard = 0;

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (_map[x, y] == '|')
                {
                    wood++;

                    continue;
                }

                if (_map[x, y] == '#')
                {
                    yard++;
                }
            }
        }

        return wood * yard;
    }

    protected void RunCycle()
    {
        var newMap = new char[Width, Height];

        Buffer.BlockCopy(_map, 0, newMap, 0, Width * Height * sizeof(char));

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                switch (_map[x, y])
                {
                    case '.':
                        if (CountAdjacent(x, y, '|') > 2)
                        {
                            newMap[x, y] = '|';
                        }

                        break;
                    case '|':
                        if (CountAdjacent(x, y, '#') > 2)
                        {
                            newMap[x, y] = '#';
                        }

                        break;
                    case '#':
                        if (CountAdjacent(x, y, '#') == 0 || CountAdjacent(x, y, '|') == 0)
                        {
                            newMap[x, y] = '.';
                        }

                        break;
                }
            }
        }

        _map = newMap;
    }

    private int CountAdjacent(int x, int y, char item)
    {
        var adjacent = 0;

        if (x > 0 && y > 0)
        {
            adjacent += _map[x - 1, y - 1] == item ? 1 : 0;
        }

        if (y > 0)
        {
            adjacent += _map[x, y - 1] == item ? 1 : 0;
        }

        if (x < Width - 1 && y > 0)
        {
            adjacent += _map[x + 1, y - 1] == item ? 1 : 0;
        }

        if (x > 0)
        {
            adjacent += _map[x - 1, y] == item ? 1 : 0;
        }

        if (x < Width - 1)
        {
            adjacent += _map[x + 1, y] == item ? 1 : 0;
        }

        if (x > 0 && y < Height - 1)
        {
            adjacent += _map[x - 1, y + 1] == item ? 1 : 0;
        }

        if (y < Height - 1)
        {
            adjacent += _map[x, y + 1] == item ? 1 : 0;
        }

        if (x < Width - 1 && y < Height - 1)
        {
            adjacent += _map[x + 1, y + 1] == item ? 1 : 0;
        }

        return adjacent;
    }

    protected void ParseInput()
    {
        _map = new char[Width, Height];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                _map[x, y] = Input[y][x];
            }
        }
    }
}