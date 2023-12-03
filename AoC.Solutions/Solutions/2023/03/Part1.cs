using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._03;

[UsedImplicitly]
public class Part1 : Base
{
    private int _width;

    private int _height;
    
    public override string GetAnswer()
    {
        var sum = 0;

        _width = Input[0].Length;

        _height = Input.Length;
        
        for (var y = 0; y < _height; y++)
        {
            var line = Input[y];

            var number = 0;

            var length = 0;
            
            for (var x = 0; x < _width; x++)
            {
                if (char.IsNumber(line[x]))
                {
                    number *= 10;

                    number += line[x] - '0';

                    length++;
                }
                else
                {
                    if (number != 0)
                    {
                        if (AdjacentSymbol(x - length, y, length))
                        {
                            sum += number;
                        }

                        number = 0;

                        length = 0;
                    }
                }
            }
        }

        return sum.ToString();
    }

    private bool AdjacentSymbol(int x, int y, int length)
    {
        for (var i = -1; i < length + 1; i++)
        {
            if (GetChar(x + i, y - 1) != '.' || GetChar(x + i, y + 1) != '.')
            {
                return true;
            }
        }
        
        if (GetChar(x - 1, y) != '.' || GetChar(x + length + 1, y) != '.')
        {
            return true;
        }

        return false;
    }

    private char GetChar(int x, int y)
    {
        if (x < 0 || x >= _width)
        {
            return '.';
        }

        if (y < 0 || y >= _height)
        {
            return '.';
        }

        if (char.IsNumber(Input[y][x]))
        {
            return '.';
        }

        return Input[y][x];
    }
}