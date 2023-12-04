using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;

        Width = Input[0].Length;

        Height = Input.Length;
        
        for (var y = 0; y < Height; y++)
        {
            var line = Input[y];

            var number = 0;

            var length = 0;
            
            for (var x = 0; x < Width; x++)
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

            if (number != 0)
            {
                if (AdjacentSymbol(Width - length, y, length))
                {
                    sum += number;
                }
            }
        }

        return sum.ToString();
    }

    private bool AdjacentSymbol(int x, int y, int length)
    {
        for (var i = x - 1; i <= x + length; i++)
        {
            if (GetChar(i, y - 1) != '.' || GetChar(i, y + 1) != '.')
            {
                return true;
            }
        }
        
        if (GetChar(x - 1, y) != '.' || GetChar(x + length, y) != '.')
        {
            return true;
        }

        return false;
    }
    
    private char GetChar(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height || char.IsNumber(Input[y][x]))
        {
            return '.';
        }

        return Input[y][x];
    }
}