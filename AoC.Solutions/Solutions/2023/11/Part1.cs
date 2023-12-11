using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._11;

[UsedImplicitly]
public class Part1 : Base
{
    private int _width;

    private int _height;
    
    private readonly List<Point> _stars = new();
    
    public override string GetAnswer()
    {
        ParseInput();
        
        ExpandUniverse();

        var result = SumShortestPaths();
        
        return result.ToString();
    }

    private int SumShortestPaths()
    {
        var sum = 0;

        for (var i = 0; i < _stars.Count; i++)
        {
            for (var j = i + 1; j < _stars.Count; j++)
            {
                var star = _stars[i];

                var other = _stars[j];
                
                sum += Math.Abs(star.X - other.X + (star.Y - other.Y));
                
                Console.WriteLine($"{i + 1} => {j + 1}: {Math.Abs(star.X - other.X + (star.Y - other.Y))}");
            }
        }
        
        return sum;
    }

    private void ExpandUniverse()
    {
        for (var y = _height; y > 0; y--)
        {
            if (_stars.All(s => s.Y != y))
            {
                foreach (var star in _stars.Where(s => s.Y > y))
                {
                    star.Y++;
                }
            }
        }

        for (var x = _width; x > 0; x--)
        {
            if (_stars.All(s => s.X != x))
            {
                foreach (var star in _stars.Where(s => s.X > x))
                {
                    star.X++;
                }
            }
        }
    }

    private void ParseInput()
    {
        _height = Input.Length;

        _width = Input[0].Length;
        
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (Input[y][x] != '.')
                {
                    _stars.Add(new Point(x, y));
                }
            }
        }
    }
}