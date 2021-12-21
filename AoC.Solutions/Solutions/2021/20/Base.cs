#define DUMP
using System.Text;
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._20;

public abstract class Base : Solution
{
    public override string Description => "NCIS image enhancement";

    private bool[] _algorithm;

    private readonly List<Point> _lightPixels = new();

    private List<Point> _pixelsToFlip;

    protected void ParseInput()
    {
        _algorithm = Input[0].Select(c => c == '#').ToArray();

        var y = 0;

        foreach (var line in Input.Skip(2))
        {
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    _lightPixels.Add(new Point(x, y));
                }
            }

            y++;
        }
    }

    protected void Enhance()
    {
        _pixelsToFlip = new List<Point>();

        foreach (var pixel in _lightPixels)
        {
            EvaluatePixel(new Point(pixel.X - 1, pixel.Y - 1));

            EvaluatePixel(new Point(pixel.X, pixel.Y - 1));

            EvaluatePixel(new Point(pixel.X + 1, pixel.Y - 1));

            EvaluatePixel(new Point(pixel.X - 1, pixel.Y));

            EvaluatePixel(new Point(pixel.X, pixel.Y));

            EvaluatePixel(new Point(pixel.X + 1, pixel.Y));

            EvaluatePixel(new Point(pixel.X - 1, pixel.Y + 1));

            EvaluatePixel(new Point(pixel.X, pixel.Y + 1));

            EvaluatePixel(new Point(pixel.X + 1, pixel.Y + 1));
        }

        ApplyEnhancement();

#if DUMP && DEBUG
        Dump();
#endif
    }

    private void ApplyEnhancement()
    {
        foreach (var pixel in _pixelsToFlip)
        {
            if (_lightPixels.Contains(pixel))
            {
                _lightPixels.Remove(pixel);
            }
            else
            {
                _lightPixels.Add(pixel);
            }
        }
    }

    private void EvaluatePixel(Point pixel)
    {
        var indexBinary = new StringBuilder();

        indexBinary.Append(_lightPixels.Any(p => p.Equals(new Point(pixel.X - 1, pixel.Y - 1))) ? '1' : '0');

        indexBinary.Append(_lightPixels.Any(p => p.Equals(new Point(pixel.X, pixel.Y - 1))) ? '1' : '0');

        indexBinary.Append(_lightPixels.Any(p => p.Equals(new Point(pixel.X + 1, pixel.Y - 1))) ? '1' : '0');

        indexBinary.Append(_lightPixels.Any(p => p.Equals(new Point(pixel.X - 1, pixel.Y))) ? '1' : '0');

        indexBinary.Append(_lightPixels.Any(p => p.Equals(new Point(pixel.X, pixel.Y))) ? '1' : '0');

        indexBinary.Append(_lightPixels.Any(p => p.Equals(new Point(pixel.X + 1, pixel.Y))) ? '1' : '0');

        indexBinary.Append(_lightPixels.Any(p => p.Equals(new Point(pixel.X - 1, pixel.Y + 1))) ? '1' : '0');

        indexBinary.Append(_lightPixels.Any(p => p.Equals(new Point(pixel.X, pixel.Y + 1))) ? '1' : '0');

        indexBinary.Append(_lightPixels.Any(p => p.Equals(new Point(pixel.X + 1, pixel.Y + 1))) ? '1' : '0');

        var index = Convert.ToInt32(indexBinary.ToString(), 2);

        if (_algorithm[index])
        {
            if (! _lightPixels.Contains(pixel) && ! _pixelsToFlip.Contains(pixel))
            {
                _pixelsToFlip.Add(pixel);
            }
        }
        else
        {
            if (_lightPixels.Contains(pixel) && ! _pixelsToFlip.Contains(pixel))
            {
                _pixelsToFlip.Add(pixel);
            }
        }
    }

#if DUMP && DEBUG
    private void Dump()
    {
        var yMin = _lightPixels.Min(p => p.Y);

        var xMin = _lightPixels.Min(p => p.X);

        var yMax = _lightPixels.Max(p => p.Y);

        var xMax = _lightPixels.Max(p => p.X);

        for (var y = yMin; y <= yMax; y++)
        {
            for (var x = xMin; x <= xMax; x++)
            {
                Console.Write(_lightPixels.Any(p => p.X == x && p.Y == y) ? '#' : '.');
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
#endif
}