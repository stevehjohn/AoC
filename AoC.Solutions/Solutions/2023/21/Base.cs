using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._21;

public abstract class Base : Solution
{
    public override string Description => "Step counter";

    protected char[,] Map;

    protected int Width;

    protected int Height;

    protected (int X, int Y) ParseInput()
    {
        Width = Input[0].Length;

        Height = Input.Length;
        
        Map = new char[Width, Height];

        var y = 0;

        (int X, int Y) start = (0, 0);
        
        foreach (var line in Input)
        {
            for (var x = 0; x < line.Length; x++)
            {
                Map[x, y] = line[x];

                if (line[x] == 'S')
                {
                    start = (x, y);
                    Map[x, y] = '.';
                }
            }

            y++;
        }

        return start;
    }
}