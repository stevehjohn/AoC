using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._22;

public abstract class Base : Solution
{
    public override string Description => "Caving capers";

    protected char[,] Map;

    protected int TargetX;

    protected int TargetY;

    protected int Width;

    protected int Height;

    private int _depth;

    protected void GenerateMap(int sizeMultiplier = 1)
    {
        Width = (TargetX + 1) * sizeMultiplier;

        Height = (TargetY + 1) * sizeMultiplier;

        var indexes = new int[Width, Height];

        Map = new char[Width, Height];

        Map[0, 0] = 'M';

        Map[TargetX, TargetY] = 'T';

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (x == 0 && y == 0 || x == TargetX && y == TargetY)
                {
                    continue;
                }

                int index;

                if (y == 0)
                {
                    index = x * 16807;
                }
                else if (x == 0)
                {
                    index = y * 48271;
                }
                else
                {
                    index = indexes[x - 1, y] * indexes[x, y - 1];
                }

                var erosion = (index + _depth) % 20183;

                indexes[x, y] = erosion;

                var type = erosion % 3;

                Map[x, y] = type == 0 ? '.' : type == 1 ? '=' : '|';
            }
        }
    }

    protected void ParseInput()
    {
        _depth = int.Parse(Input[0][7..]);

        var parts = Input[1][8..].Split(',', StringSplitOptions.TrimEntries);

        TargetX = int.Parse(parts[0]);

        TargetY = int.Parse(parts[1]);
    }
}