using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._17;

[UsedImplicitly]
public class Part1 : Base
{
    private int _springX = 500;

    private char[,] _map;

    public override string GetAnswer()
    {
        Console.CursorVisible = false;

        ParseInput();

        Dump();

        while (true)
        {
            DropWater(1);
            
            DropWater(-1);
         
            Thread.Sleep(500);
        }

        return "TESTING";
    }

    private void DropWater(int direction)
    {
        var x = _springX;

        var y = 1;

        while (true)
        {
            Dump(x, y);

            if (_map[x, y + 1] == '\0')
            {
                y++;

                continue;
            }

            var left = -1;

            for (var tX = x; tX >= 0; tX--)
            {
                if (_map[tX, y] != '\0')
                {
                    left = tX + 1;

                    break;
                }
            }

            var right = -1;

            for (var tX = x; tX < _map.GetLength(0); tX++)
            {
                if (_map[tX, y] != '\0')
                {
                    right = tX - 1;

                    break;
                }
            }

            if (left == -1 || right == -1)
            {
                x += direction;

                continue;
            }

            for (var i = left; i <= right; i++)
            {
                _map[i, y] = '~';
            }

            return;
        }
    }

    private void Dump(int wX = -1, int wY = -1)
    {
        Console.SetCursorPosition(0, 1);

        for (var y = 0; y < _map.GetLength(1); y++)
        {
            for (var x = 0; x < _map.GetLength(0); x++)
            {
                if (wX == x && wY == y)
                {
                    Console.Write('*');

                    continue;
                }

                Console.Write(_map[x, y] == '\0' ? ' ' : _map[x, y]);
            }

            Console.WriteLine('|');
        }

        Console.WriteLine();
    }

    private void ParseInput()
    {
        var boundaries = FindBoundaries();

        _springX -= boundaries.XMin - 1;

        // Need +1 either side for overspill. Account for this when parsing the input.
        _map = new char[boundaries.XMax - boundaries.XMin + 3, boundaries.YMax + 1];

        _map[_springX, 0] = '+';

        foreach (var line in Input)
        {
            var data = ParseLine(line);

            for (var i = data.RangeStart; i <= data.RangeEnd; i++)
            {
                if (data.RangeAxis == 'x')
                {
                    _map[i - boundaries.XMin + 1, data.Single] = '#';
                }
                else
                {
                    _map[data.Single - boundaries.XMin + 1, i] = '#';
                }
            }
        }
    }

    private (int XMin, int XMax, int YMin, int YMax) FindBoundaries()
    {
        var xMin = int.MaxValue;

        var yMin = int.MaxValue;

        var xMax = int.MinValue;

        var yMax = int.MinValue;

        foreach (var line in Input)
        {
            var data = ParseLine(line);

            if (data.RangeAxis == 'y')
            {
                xMin = Math.Min(data.Single, xMin);

                xMax = Math.Max(data.Single, xMax);

                yMin = Math.Min(data.RangeStart, yMin);

                yMax = Math.Max(data.RangeEnd, yMax);
            }
            else
            {
                xMin = Math.Min(data.RangeStart, xMin);

                xMax = Math.Max(data.RangeEnd, xMax);

                yMin = Math.Min(data.Single, yMin);

                yMax = Math.Max(data.Single, yMax);
            }
        }

        return (xMin, xMax, yMin, yMax);
    }

    private static (int Single, int RangeStart, int RangeEnd, char RangeAxis) ParseLine(string line)
    {
        var halves = line.Split(',', StringSplitOptions.TrimEntries);

        var rangeAxis = halves[1][0];

        var single = int.Parse(halves[0][2..]);

        var range = halves[1][2..].Split("..", StringSplitOptions.TrimEntries);

        var rangeStart = Math.Min(int.Parse(range[0]), int.Parse(range[1]));
        
        var rangeEnd = Math.Max(int.Parse(range[0]), int.Parse(range[1]));

        return (single, rangeStart, rangeEnd, rangeAxis);
    }
}