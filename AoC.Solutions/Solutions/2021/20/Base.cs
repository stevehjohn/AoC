using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._20;

public abstract class Base : Solution
{
    private int _padBy;

    public override string Description => "NCIS image enhancement";

    private bool[] _algorithm;

    private int _width;

    private int _height;

    private bool[,] _image;

    private readonly HashSet<Point> _pixelsToFlip = new();

    private bool _infinityLit;

    public string GetAnswer(int iterations)
    {
        _padBy = iterations;

        ParseInput();

        for (var i = 0; i < iterations; i++)
        {
            Enhance();
        }

        return CountLitPixels().ToString();
    }

    protected void ParseInput()
    {
        _algorithm = Input[0].Select(c => c == '#').ToArray();

        var y = 0;

        _width = Input[2].Length + _padBy * 2;

        _height = Input.Length - 2 + _padBy * 2;

        _image = new bool[_width, _height];

        foreach (var line in Input.Skip(2))
        {
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    _image[x + _padBy, y + _padBy] = true;
                }
            }

            y++;
        }

#if DEBUG && DUMP
        Dump();
#endif
    }

    public void Enhance()
    {
        _pixelsToFlip.Clear();

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                EnhancePixel(x, y);
            }
        }

        Apply();

        if (_algorithm[0])
        {
            _infinityLit = ! _infinityLit;
        }

#if DEBUG && DUMP
        Dump();
#endif
    }

    public int CountLitPixels()
    {
        var lit = 0;

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                lit += _image[x, y] ? 1 : 0;
            }
        }

        return lit;
    }

    private void Apply()
    {
        foreach (var pixel in _pixelsToFlip)
        {
            _image[pixel.X, pixel.Y] = ! _image[pixel.X, pixel.Y];
        }
    }

    private void EnhancePixel(int cX, int cY)
    {
        var index = 0;

        var shift = 0;

        for (var y = cY - 1; y < cY + 2; y++)
        {
            for (var x = cX - 1; x < cX + 2; x++)
            {
                if (x < 0 || x >= _width || y < 0 || y >= _height)
                {
                    index += _infinityLit ? 256 >> shift : 0;
                }
                else
                {
                    index += _image[x, y] ? 256 >> shift : 0;
                }

                shift++;
            }
        }

        var pixel = new Point(cX, cY);

        if (_algorithm[index] != _image[cX, cY])
        {
            _pixelsToFlip.Add(pixel);
        }
    }

#if DEBUG && DUMP
    private void Dump()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                Console.Write(_image[x, y] ? '#' : '.');
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
#endif
}