namespace AoC.Solutions.Infrastructure;

public sealed class Map
{
    private readonly int _width;

    private readonly int _height;

    private readonly char[] _cells;

    public readonly int Length;

    public char this[int index]
    {
        get => _cells[index];
        
        set => _cells[index] = value;
    }

    public Map(string[] input)
    {
        _width = input[0].Length;

        _height = input.Length;
        
        Length = _width * _height;

        _cells = new char[Length];

        var i = 0;

        foreach (var line in input)
        {
            for (var x = 0; x < _width; x++)
            {
                _cells[i++] = line[x];
            }
        }
    }

    public int CountAdjacentCells(int index, char value)
    {
        var x = index % _width;

        var y = index / _width;

        var hasLeft = x > 0;

        var hasRight = x < _width - 1;

        var count = 0;

        if (y > 0)
        {
            var up = index - _width;

            if (hasLeft && _cells[up - 1] == value)
            {
                count++;
            }

            if (_cells[up] == value)
            {
                count++;
            }

            if (hasRight)
            {
                if (_cells[up + 1] == value)
                {
                    count++;
                }
            }
        }

        if (hasLeft)
        {
            if (_cells[index - 1] == value)
            {
                count++;
            }
        }

        if (hasRight)
        {
            if (_cells[index + 1] == value)
            {
                count++;
            }
        }

        if (y < _height - 1)
        {
            var down = index + _width;

            if (hasLeft)
            {
                if (_cells[down - 1] == value)
                {
                    count++;
                }
            }

            if (_cells[down] == value)
            {
                count++;
            }

            if (hasRight)
            {
                if (_cells[down + 1] == value)
                {
                    count++;
                }
            }
        }

        return count;
    }
}