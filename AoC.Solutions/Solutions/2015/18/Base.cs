
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._18;

public abstract class Base : Solution
{
    public override string Description => "Bournville GIF";

    private const int GridSize = 100;
    
    private const int Stride = GridSize + 2;

    protected HashSet<Point> Lights = [];

    private readonly byte[] _grid = new byte[Stride * Stride];

    protected void RunStep(bool isPart2 = false)
    {
        Array.Clear(_grid);

        foreach (var point in Lights)
        {
            _grid[(point.Y + 1) * Stride + point.X + 1] = 1;
        }

        var next = new HashSet<Point>();

        for (var y = 1; y <= GridSize; y++)
        {
            var row = y * Stride;

            for (var x = 1; x <= GridSize; x++)
            {
                var i = row + x;

                var neighbours =
                    _grid[i - Stride - 1] +
                    _grid[i - Stride] +
                    _grid[i - Stride + 1] +
                    _grid[i - 1] +
                    _grid[i + 1] +
                    _grid[i + Stride - 1] +
                    _grid[i + Stride] +
                    _grid[i + Stride + 1];

                if (neighbours == 3 || (neighbours == 2 && _grid[i] != 0))
                {
                    next.Add(new Point(x - 1, y - 1));
                }
            }
        }

        if (isPart2)
        {
            next.Add(new Point(0, 0));
            
            next.Add(new Point(0, GridSize - 1));
            
            next.Add(new Point(GridSize - 1, 0));
            
            next.Add(new Point(GridSize - 1, GridSize - 1));
        }

        Lights = next;
    }

    protected void ParseInput()
    {
        Lights.Clear();

        for (var y = 0; y < GridSize; y++)
        {
            var line = Input[y];

            for (var x = 0; x < GridSize; x++)
            {
                if (line[x] == '#')
                {
                    Lights.Add(new Point(x, y));
                }
            }
        }
    }
}
