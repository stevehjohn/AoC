using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._08;

public abstract class Base : Solution
{
    public override string Description => "Resonant collinearity";
    
    protected readonly HashSet<(int X, int Y)> AntiNodes = [];

    private readonly List<(int X, int Y, char Frequency)> _antennas = [];

    private int _width;

    private int _height;
    
    protected void LocateNodes()
    {
        _width = Input[0].Length;

        _height = Input.Length;
        
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (Input[y][x] != '.')
                {
                    _antennas.Add((x, y, Input[y][x]));
                }
            }
        }
    }

    protected void CalculateAntiNodes()
    {
        foreach (var left in _antennas)
        {
            foreach (var right in _antennas)
            {
                if (left.Frequency != right.Frequency || left == right)
                {
                    continue;
                }

                var dX = right.X - left.X;

                var dY = right.Y - left.Y;

                AddAntiNode(left, dX, dY);
            }
        }
    }

    private void AddAntiNode((int X, int Y, char Frequency) antenna, int dX, int dY)
    {
        var x = antenna.X - dX;

        if (x < 0 || x >= _width)
        {
            return;
        }

        var y = antenna.Y - dY;

        if (y < 0 || y >= _height)
        {
            return;
        }
        
        AntiNodes.Add((x, y));
    }
}