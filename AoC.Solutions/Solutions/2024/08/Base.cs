using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._08;

public abstract class Base : Solution
{
    public override string Description => "Resonant collinearity";
    
    protected readonly HashSet<(int X, int Y)> AntiNodes = [];

    private readonly List<(int X, int Y, char Frequency)> _antennas = [];

    private int _width;

    private int _height;
    
    protected void LocateNodes(bool createAntiNode = false)
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

                    if (createAntiNode)
                    {
                        AntiNodes.Add((x, y));
                    }
                }
            }
        }
    }

    protected void CalculateAntiNodes(bool repeat = false)
    {
        for (var left = 0; left < _antennas.Count; left++)
        {
            for (var right = 0; right < _antennas.Count; right++)
            {
                if (left == right)
                {
                    continue;
                }

                var leftAntenna = _antennas[left];

                var rightAntenna = _antennas[right];

                if (leftAntenna.Frequency != rightAntenna.Frequency)
                {
                    continue;
                }

                var dX = rightAntenna.X - leftAntenna.X;

                var dY = rightAntenna.Y - leftAntenna.Y;

                AddAntiNode(leftAntenna, dX, dY, repeat);
            }
        }
    }

    private void AddAntiNode((int X, int Y, char Frequency) antenna, int dX, int dY, bool repeat)
    {
        var x = antenna.X;

        var y = antenna.Y;
        
        do
        {
            x -= dX;

            if (x < 0 || x >= _width)
            {
                return;
            }

            y -= dY;

            if (y < 0 || y >= _height)
            {
                return;
            }

            AntiNodes.Add((x, y));
            
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop - Bounds checks exit loop.
        } while (repeat);
    }
}