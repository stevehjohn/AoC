using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Libraries;

namespace AoC.Solutions.Solutions._2023._11;

public abstract class Base : Solution
{
    public override string Description => "Cosmic expansion";

    private int _width;

    private int _height;
    
    private readonly List<Point> _stars = [];

    private readonly HashSet<int> _columns = [];

    private readonly HashSet<int> _rows = [];
    
    protected long SumShortestPaths()
    {
        var sum = 0L;

        for (var i = 0; i < _stars.Count; i++)
        {
            for (var j = i + 1; j < _stars.Count; j++)
            {
                var star = _stars[i];

                var other = _stars[j];

                sum += Measurement.GetManhattanDistance(star.X, star.Y, other.X, other.Y);
            }
        }
        
        return sum;
    }

    protected void ExpandUniverse(int expansion = 1)
    {
        for (var y = _height; y > 0; y--)
        {
            if (! _rows.Contains(y))
            {
                foreach (var star in _stars.Where(s => s.Y > y))
                {
                    star.Y += expansion;
                }
            }
        }

        for (var x = _width; x > 0; x--)
        {
            if (! _columns.Contains(x))
            {
                foreach (var star in _stars.Where(s => s.X > x))
                {
                    star.X += expansion;
                }
            }
        }
    }

    protected void ParseInput()
    {
        _height = Input.Length;

        _width = Input[0].Length;
        
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (Input[y][x] != '.')
                {
                    _columns.Add(x);

                    _rows.Add(y);
                    
                    _stars.Add(new Point(x, y));
                }
            }
        }
    }
}