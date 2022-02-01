using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._17;

[UsedImplicitly]
public class Part1 : Base
{
    private int _springX = 500;

    private char[,] _map;

    private int _width;

    public override string GetAnswer()
    {
        Console.CursorVisible = false;

        ParseInput();

        while (true)
        {
            DropWater();
        }

        return "TESTING";
    }

    private void DropWater()
    {
        var x = _springX;

        var y = 1;

        _map[x, y] = '|';

        var lastX = -1;

        var lastY = -1;

        var direction = -1;

        var changed = false;

        while (true)
        {
            Dump(x, y);

            Thread.Sleep(10);

            if (_map[x, y + 1] is '\0' or '|')
            {
                y++;

                _map[x, y] = '|';

                continue;
            }

            if (Floods(x, y))
            {
                return;
            }
        }
    }

    private bool Floods(int x, int y)
    {
        int oX;

        for (oX = x; oX >= 0; oX--)
        {
            if (_map[oX, y + 1] is '\0' or '|')
            {
                return false;
            }

            if (_map[oX, y] == '#')
            {
                break;
            }
        }

        var left = oX + 1;

        for (oX = x; oX < _width; oX++)
        {
            if (_map[oX, y + 1] is '\0' or '|')
            {
                return false;
            }

            if (_map[oX, y] == '#')
            {
                break;
            }
        }

        var right = oX - 1;

        for (oX = left; oX <= right; oX++)
        {
            _map[oX, y] = '~';
        }

        return true;
    }

    private void Dump(int wX, int wY)
    {
        Console.SetCursorPosition(0, 1);

        var start = Math.Max(0, wY - 10);

        for (var y = start; y < start + 70; y++)
        {
            var builder = new StringBuilder();

            for (var x = 0; x < _width; x++)
            {
                if (wX == x && wY == y)
                {
                    builder.Append('*');

                    continue;
                }

                builder.Append(_map[x, y] == '\0' ? ' ' : _map[x, y]);
            }

            Console.WriteLine($"{builder}|");
        }
    }

    private void ParseInput()
    {
        var boundaries = FindBoundaries();

        _springX -= boundaries.XMin - 1;

        _map = new char[boundaries.XMax - boundaries.XMin + 3, boundaries.YMax + 1];

        _width = boundaries.XMax - boundaries.XMin + 3;

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