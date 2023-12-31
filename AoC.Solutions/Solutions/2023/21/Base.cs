using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._21;

public abstract class Base : Solution
{
    public override string Description => "Step counter";

    private const long TargetSteps = 26_501_365;

    private char[,] _map;

    private int _width;

    private int _height;

    protected long ExtrapolateAnswer()
    {
        var halfWidth = _width / 2;

        var countHalfWidth = CountAtStep(halfWidth);

        var countWidthPlusHalf = CountAtStep(halfWidth + _width);
        
        var delta1 = countWidthPlusHalf - countHalfWidth;

        var delta2 = CountAtStep(halfWidth + _width * 2) - countWidthPlusHalf;

        var quotient = TargetSteps / _width;

        var result = countHalfWidth + delta1 * quotient + quotient * (quotient - 1) / 2 * (delta2 - delta1);

        return result;
    }

    protected void FillUnreachable()
    {
        for (var y = 1; y < _height - 2; y++)
        {
            for (var x = 1; x < _width - 2; x++)
            {
                if (_map[x - 1, y] == '#' && _map[x + 1, y] == '#' && _map[x, y - 1] == '#' && _map[x, y + 1] == '#')
                {
                    _map[x, y] = '#';
                }
            }
        }
    }

    protected int CountAtStep(int step)
    {
        var count = 0;

        var yD = 0;

        for (var x = -step; x <= step; x++)
        {
            for (var y = -yD; y <= yD; y += 2)
            {
                if (CheckMap(65 + x, 65 + y))
                {
                    count++;
                }
            }

            if (x < 0)
            {
                yD++;
            }
            else
            {
                yD--;
            }
        }

        return count;
    }

    private bool CheckMap(int x, int y)
    {
        while (x < 0)
        {
            x += 131;
        }

        while (x > 130)
        {
            x -= 131;
        }

        while (y < 0)
        {
            y += 131;
        }

        while (y > 130)
        {
            y -= 131;
        }

        return _map[x, y] != '#';
    }
    
    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;
        
        _map = new char[_width, _height];

        var y = 0;

        foreach (var line in Input)
        {
            for (var x = 0; x < line.Length; x++)
            {
                _map[x, y] = line[x];

                if (line[x] == 'S')
                {
                    _map[x, y] = '.';
                }
            }

            y++;
        }
    }
}