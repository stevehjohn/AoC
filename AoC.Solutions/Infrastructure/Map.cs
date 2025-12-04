namespace AoC.Solutions.Infrastructure;

public sealed class Map
{
    private readonly int _width;

    private readonly int _height;

    private readonly int _length;

    private readonly char[] _cells;

    public char this[int index]
    {
        set => _cells[index] = value;
    }

    public Map(string[] input)
    {
        _width = input[0].Length;

        _height = input.Length;

        _length = _width * _height;

        _cells = new char[_length];

        var i = 0;

        foreach (var line in input)
        {
            for (var x = 0; x < _width; x++)
            {
                _cells[i++] = line[x];
            }
        }
    }

    public void ForAllCells(Action<int, char> action)
    {
        for (var i = 0; i < _length; i++)
        {
            action(i, _cells[i]);
        }
    }

    public void ForAdjacentCells(int index, Action<char> action)
    {
        var x = index % _width;

        var y = index / _width;

        var hasLeft = x > 0;

        var hasRight = x < _width - 1;

        if (y > 0)
        {
            var up = index - _width;

            if (hasLeft)
            {
                action(_cells[up - 1]);
            }

            action(_cells[up]);

            if (hasRight)
            {
                action(_cells[up + 1]);
            }
        }

        if (hasLeft)
        {
            action(_cells[index - 1]);
        }

        if (hasRight)
        {
            action(_cells[index + 1]);
        }

        if (y < _height - 1)
        {
            var down = index + _width;

            if (hasLeft)
            {
                action(_cells[down - 1]);
            }

            action(_cells[down]);

            if (hasRight)
            {
                action(_cells[down + 1]);
            }
        }
    }
}