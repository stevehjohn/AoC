using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._08;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<(int X, int Y, char Frequency)> _antennas = [];

    private readonly HashSet<(int X, int Y)> _antiNodes = [];

    private int _width;

    private int _height;
    
    public override string GetAnswer()
    {
        LocateNodes();

        CalculateAntiNodes();

        return _antiNodes.Count.ToString();
    }

    private void CalculateAntiNodes()
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
        
        _antiNodes.Add((x, y));
    }

    private void LocateNodes()
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
}