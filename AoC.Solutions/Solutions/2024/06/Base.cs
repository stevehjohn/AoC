using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._06;

public abstract class Base : Solution
{
    public override string Description => "Guard gallivant";

    protected int Width;

    protected int Height;

    protected char[,] Map;

    protected (int X, int Y) StartPosition;

    protected readonly HashSet<int> Visited = [];

    private bool[,,,] _turns;

    protected void ParseInput()
    {
        Map = Input.To2DArray();

        Width = Map.GetUpperBound(0);

        Height = Map.GetUpperBound(1);

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                if (Map[x, y] == '^')
                {
                    StartPosition = (x, y);
                    
                    break;
                }
            }
        }

        _turns = new bool[Width + 1, Height + 1, 3, 3];
    }

    protected int WalkMap(bool detectLoops = false)
    {
        var x = StartPosition.X;

        var y = StartPosition.Y;

        var dX = 0;

        var dY = -1;
        
        Array.Clear(_turns);
        
        while (true)
        {
            x += dX;

            y += dY;

            if (x < 0 || x > Width || y < 0 || y > Height)
            {
                break;
            }

            if (Map[x, y] == '#')
            {
                if (detectLoops && _turns[x, y, dX + 1, dY + 1])
                {
                    return -1;
                }

                _turns[x, y, dX + 1, dY + 1] = true;

                x -= dX;

                y -= dY;

                (dX, dY) = (-dY, dX);

                continue;
            }

            if (! detectLoops)
            {
                Visited.Add(x + y * Width);
            }
        }

        return Visited.Count;
    }
}