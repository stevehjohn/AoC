using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._09;

[UsedImplicitly]
public class Part2 : Base
{
    private bool[,] _basin;

    public override string GetAnswer()
    {
        ParseInput();

        var basins = new List<int>();

        for (var y = 1; y <= Height - 2; y++)
        {
            for (var x = 1; x <= Width - 2; x++)
            {
                var point = Map[x, y];

                if (point < Map[x, y - 1]
                    && point < Map[x - 1, y]
                    && point < Map[x + 1, y]
                    && point < Map[x, y + 1])
                {
                    basins.Add(FindBasin(x, y));
                }
            }
        }

        basins = basins.OrderByDescending(b => b).ToList();

        return (basins[0] * basins[1] * basins[2]).ToString();
    }

    private int FindBasin(int x, int y)
    {
        _basin = new bool[Width, Height];

        _basin[x, y] = true;

        Spread(Map[x, y], x, y);

        var count = 0;

        for (var ix = 0; ix < Width; ix++)
        {
            for (var iy = 0; iy < Height; iy++)
            {
                if (_basin[ix, iy])
                {
                    count++;
                }
            }
        }

        return count;
    }

    private void Spread(int value, int x, int y)
    {
        if (x > 1 && Map[x - 1, y] > value && Map[x - 1, y] < 9)
        {
            _basin[x - 1, y] = true;

            Spread(Map[x - 1, y], x - 1, y);
        }

        if (x < Width - 2 && Map[x + 1, y] > value && Map[x + 1, y] < 9)
        {
            _basin[x + 1, y] = true;

            Spread(Map[x + 1, y], x + 1, y);
        }

        if (y > 1 && Map[x, y - 1] > value && Map[x, y - 1] < 9)
        {
            _basin[x, y - 1] = true;

            Spread(Map[x, y - 1], x, y - 1);
        }

        if (y < Height - 2 && Map[x, y + 1] > value && Map[x, y + 1] < 9)
        {
            _basin[x, y + 1] = true;

            Spread(Map[x, y + 1], x, y + 1);
        }
    }
}