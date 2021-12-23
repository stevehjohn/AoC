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

    protected void ParseInput()
    {
        _algorithm = Input[0].Select(c => c == '#').ToArray();

        var y = 1;

        _width = Input[2].Length + 4;

        _height = Input.Length + 2;

        _image = new bool[_width, _height];

        foreach (var line in Input.Skip(2))
        {
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    _image[x + 2, y + 1] = true;
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

        for (var y = 1; y < _height - 2; y++)
        {
            for (var x = 1; x < _width - 2; x++)
            {
                //if (_image[x, y]) Debugger.Break();

                //if (x == 4 && y == 4) Debugger.Break();
                EnhancePixel(x, y);
            }
        }

        Apply();

#if DEBUG && DUMP
        Dump();
#endif
    }

    private void Apply()
    {
        var newImage = new bool[_width + 4, _height + 4];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                newImage[x + 2, y + 2] = _image[x, y];
            }
        }

        foreach (var pixel in _pixelsToFlip)
        {
            newImage[pixel.X + 2, pixel.Y + 2] = ! newImage[pixel.X + 2, pixel.Y + 2];
        }

        _image = newImage;

        _width += 4;

        _height += 4;
    }

    private void EnhancePixel(int cX, int cY)
    {
        var index = 0;

        var shift = 10;

        for (var y = cY - 1; y < cY + 2; y++)
        {
            for (var x = cX - 1; x < cX + 2; x++)
            {
                index += _image[x, y] ? 256 >> shift : 0;

                shift--;
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