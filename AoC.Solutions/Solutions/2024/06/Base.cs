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

    private readonly HashSet<(int, int, int, int)> _turns = [];

    private (int X, int Y) _position;

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
                    _position = (x, y);

                    StartPosition = (x, y);
                    
                    break;
                }
            }
        }
    }

    protected int WalkMap(bool detectLoops = false)
    {
        var x = _position.X;

        var y = _position.Y;

        var dX = 0;

        var dY = -1;
        
        _turns.Clear();
        
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
                if (detectLoops && ! _turns.Add((x, y, dX, dY)))
                {
                    return -1;
                }

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