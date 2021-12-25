#define DUMP
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._20;

public abstract class Base : Solution
{
    public override string Description => "NCIS image enhancement";

    private bool[] _algorithm;

    private int _width;

    private int _height;

    private bool[,] _image;

    private readonly List<Point> _pixelsToFlip = new();

    private bool _infinityLit;

    public string GetAnswer(int iterations)
    {
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

        const int padBy = 3;

        _width = Input[2].Length + padBy * 2;

        _height = Input.Length - 2 + padBy * 2;

        _image = new bool[_width, _height];

        foreach (var line in Input.Skip(2))
        {
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    _image[x + padBy, y + padBy] = true;
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
        const int growBy = 3;

        var newImage = new bool[_width + growBy * 2, _height + growBy * 2];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                newImage[x + growBy, y + growBy] = _image[x, y];
            }
        }

        foreach (var pixel in _pixelsToFlip)
        {
            newImage[pixel.X + growBy, pixel.Y + growBy] = ! newImage[pixel.X + growBy, pixel.Y + growBy];
        }

        _image = newImage;

        _width += growBy * 2;

        _height += growBy * 2;
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

        if (_algorithm[index] != _image[cX, cY] && ! _pixelsToFlip.Contains(pixel))
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