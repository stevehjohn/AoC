using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._18;

public abstract class Base : Solution
{
    public override string Description => "I'm a lumberjack...";

    protected const int Width = 50;

    protected const int Height = 50;

    protected char[,] Map;

    protected void RunCycle()
    {
        var newMap = new char[Width, Height];

        Buffer.BlockCopy(Map, 0, newMap, 0, Width * Height * sizeof(char));

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                switch (Map[x, y])
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

        Map = newMap;
    }

    private int CountAdjacent(int x, int y, char item)
    {
        var adjacent = 0;

        if (x > 0 && y > 0)
        {
            adjacent += Map[x - 1, y - 1] == item ? 1 : 0;
        }

        if (y > 0)
        {
            adjacent += Map[x, y - 1] == item ? 1 : 0;
        }

        if (x < Width - 1 && y > 0)
        {
            adjacent += Map[x + 1, y - 1] == item ? 1 : 0;
        }

        if (x > 0)
        {
            adjacent += Map[x - 1, y] == item ? 1 : 0;
        }

        if (x < Width - 1)
        {
            adjacent += Map[x + 1, y] == item ? 1 : 0;
        }

        if (x > 0 && y < Height - 1)
        {
            adjacent += Map[x - 1, y + 1] == item ? 1 : 0;
        }

        if (y < Height - 1)
        {
            adjacent += Map[x, y + 1] == item ? 1 : 0;
        }

        if (x < Width - 1 && y < Height - 1)
        {
            adjacent += Map[x + 1, y + 1] == item ? 1 : 0;
        }

        return adjacent;
    }

    protected void ParseInput()
    {
        Map = new char[Width, Height];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                Map[x, y] = Input[y][x];
            }
        }
    }
}